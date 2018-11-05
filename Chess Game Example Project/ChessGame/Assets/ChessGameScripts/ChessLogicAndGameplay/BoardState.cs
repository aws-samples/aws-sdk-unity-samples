using System;
using System.Collections.Generic;
using System.Text;

namespace AWSSDK.Examples.ChessGame
{
    // Contains the current state of an individual chess game. This class handles most of the game logic,
    // including loading game state from Forsyth-Edwards Notation (https://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation) and Long Algebraic Notation (https://en.wikipedia.org/wiki/Algebraic_notation_%28chess%29#Long_algebraic_notation) of the
    // previous move, applying a move, and determining legal moves. 
    public class BoardState
    {
        //TODO: in general most of the methods of this class should be implemented on server side
        //TODO: I commented only some of them - those that are called from BoardUI - others should be sequentially moved to server
        
        // Refers to count the following sections:
        // positions, player turn, castling, en passant target, halfmove, fullmove.
        private const int FenSectionCount = 6;
        // Used for parseing the Algebraic Notation.
        private const int MinimumAlgebraicNotationLength = 5;
        // Constants for important Piece positions.
        private const int FirstRowWhite = 0;
        private const int FirstRowBlack = 7;
        private const int KingStartColumn = 4;
        private const int KingsideRookColumn = 7;
        private const int QueensideRookColumn = 0;
        private const int KingsideCastledKingColumn = 6;
        private const int QueensideCastledKingColumn = 2;
        private const int KingsideCastledRookColumn = 5;
        private const int QueensideCastledRookColumn = 3;
        public const string InitialGameFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        public const string InitialLongAlgNotation = "";
        private const string QueensideCastlingString = "0-0-0";
        private const string KingsideCastlingString = "0-0";
        public enum ChessPieceType
        {
            None, Rook, Knight, Bishop, Queen, King, Pawn
        }
        public enum ChessPieceColor
        {
            None, White, Black
        }
        // The directions that a rook can move.
        public static readonly List<int[]> RookDirections = new List<int[]> { new int[2] { 0, 1 }, new int[2] { 0, -1 }, new int[2] { -1, 0 }, new int[2] { 1, 0 } };
        // The directions that a bishop can move.
        public static readonly List<int[]> BishopDirections = new List<int[]> { new int[2] { 1, 1 }, new int[2] { 1, -1 }, new int[2] { -1, 1 }, new int[2] { -1, -1 } };
        // The directions that a queen can move, as well as the translational movement of a king.
        public static readonly List<int[]> QueenDirectionsKingTranslations = new List<int[]> { new int[2] { 0, 1 }, new int[2] { 0, -1 }, new int[2] { -1, 0 }, new int[2] { 1, 0 }, new int[2] { 1, 1 }, new int[2] { 1, -1 }, new int[2] { -1, 1 }, new int[2] { -1, -1 } };
        // The distances in x,y that a rook can move.
        public static readonly List<int[]> KnightTranslations = new List<int[]> { new int[2] { 2, 1 }, new int[2] { 1, 2 }, new int[2] { -2, 1 }, new int[2] { 1, -2 }, new int[2] { 2, -1 }, new int[2] { -1, 2 }, new int[2] { -2, -1 }, new int[2] { -1, -2 } };
        // Note that the keys are all uppercase, so lowercase letters should be cast to upper to retrieve the piece type
        private static readonly Dictionary<char, ChessPieceType> CharToChessPieceType = new Dictionary<char, ChessPieceType>{
            { 'P', ChessPieceType.Pawn   },
            { 'N', ChessPieceType.Knight },
            { 'B', ChessPieceType.Bishop },
            { 'Q', ChessPieceType.Queen  },
            { 'R', ChessPieceType.Rook   },
            { 'K', ChessPieceType.King   }
        };
        // Indexed by the ChessPieceType enum to retrieve the capital version of the letter that describes that piece. None Type returns a space because it has no corresponding letter. Note that in some cases a pawn is implied if there is no letter provided.
        private const string ChessPieceToCapitalChar = " RNBQKP";
        private static readonly Dictionary<char, int> FileToColumn = new Dictionary<char, int>
        {
            { 'a', 0 },
            { 'b', 1 },
            { 'c', 2 },
            { 'd', 3 },
            { 'e', 4 },
            { 'f', 5 },
            { 'g', 6 },
            { 'h', 7 },
        };
        // Indexed by the column to retrieve the file letter.
        private const string ColumnToFile = "abcdefgh";
        
        //TODO: should be stored and calculated on server
        private ChessPiece[,] BoardGrid;
        //TODO: should be stored and calculated on server
        private HashSet<ChessMove>[,] PossibleMovesGrid;

        public ChessPieceColor TurnColor { get; private set; }
        public bool WhiteKingsideCastling { get; private set; }
        public bool WhiteQueensideCastling { get; private set; }
        public bool BlackKingsideCastling { get; private set; }
        public bool BlackQueensideCastling { get; private set; }
        public int HalfMove { get; private set; }
        public int FullMove { get; private set; }
        public Coordinate EnPassantTarget { get; private set; }
        public ChessMove PreviousMove { get; private set; }

        // Take the current state of the board in Forsyth-Edwards Notation and the Long Algebraic Notation
        // of the most recent move (the FEN should already reflect the move).
        public BoardState(string forsythEdwardsNotation, string algebraicNotation)
        {
            BoardGrid = new ChessPiece[8, 8];
            // Default No color or type (i.e. an empty square)
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    this.BoardGrid[row, column] = new ChessPiece(ChessPieceColor.None, ChessPieceType.None);
                }
            }
            ParseFEN(forsythEdwardsNotation);
            ParseLongAlgebraicNotation(algebraicNotation);
            InitializeAllPossibleMoves();
        }

        // Create Board State for a new game.
        public BoardState() : this(InitialGameFen, "") { }

        // Make a new board state by applying a new move to an already existing board state.
        public BoardState(BoardState previousState, ChessMove newMove, bool lookForCheck = true)
        {
            BoardGrid = new ChessPiece[8, 8];
            if (!newMove.KingsideCastle && !newMove.QueensideCastle)
            {
                HashSet<ChessMove> previousPossibleMoves = previousState.GetPossibleMoves(newMove.From);
                if (!previousPossibleMoves.Contains(newMove))
                {
                    throw new BoardStateException("Illegal move.");
                }
            }
            // Copy elements.
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Coordinate coordinate = new Coordinate(row, column);
                    SetPieceAtCoordinate(previousState.GetPieceAtCoordinate(coordinate), coordinate);
                }
            }
            // Copy other board state values.
            BlackKingsideCastling = previousState.BlackKingsideCastling;
            BlackQueensideCastling = previousState.BlackQueensideCastling;
            WhiteKingsideCastling = previousState.WhiteKingsideCastling;
            WhiteQueensideCastling = previousState.WhiteQueensideCastling;
            // Turn color will be flipped and fullmove/halfmove will be incremented after move is applied.
            TurnColor = previousState.TurnColor;
            FullMove = previousState.FullMove;
            HalfMove = previousState.HalfMove;
            // Reset En Passant.
            EnPassantTarget = new Coordinate(-1, -1);
            // Castling special case.
            if (newMove.KingsideCastle || newMove.QueensideCastle)
            {
                int row = TurnColor == ChessPieceColor.White ? FirstRowWhite : FirstRowBlack;
                int rookStartColumn = newMove.KingsideCastle ? KingsideRookColumn : QueensideRookColumn;
                int kingEndColumn = newMove.KingsideCastle ? KingsideCastledKingColumn : QueensideCastledKingColumn;
                int rookEndColumn = newMove.KingsideCastle ? KingsideCastledRookColumn : QueensideCastledRookColumn;
                var kingStart = new Coordinate(row, KingStartColumn);
                var kingEnd = new Coordinate(row, kingEndColumn);
                var rookStart = new Coordinate(row, rookStartColumn);
                var rookEnd = new Coordinate(row, rookEndColumn);
                SetPieceAtCoordinate(new ChessPiece(ChessPieceColor.None, ChessPieceType.None), kingStart);
                SetPieceAtCoordinate(new ChessPiece(ChessPieceColor.None, ChessPieceType.None), rookStart);
                SetPieceAtCoordinate(new ChessPiece(TurnColor, ChessPieceType.King), kingEnd);
                SetPieceAtCoordinate(new ChessPiece(TurnColor, ChessPieceType.Rook), rookEnd);
                if (TurnColor == ChessPieceColor.White)
                {
                    WhiteKingsideCastling = false;
                    WhiteQueensideCastling = false;
                }
                else
                {
                    BlackKingsideCastling = false;
                    BlackQueensideCastling = false;
                }
            }
            // All other move types.
            else
            {
                // If en passant
                if (newMove.PieceType == ChessPieceType.Pawn)
                {
                    if (previousState.EnPassantTarget.Equals(newMove.To))
                    {
                        if (TurnColor == ChessPieceColor.White)
                        {
                            SetPieceAtCoordinate(new ChessPiece(ChessPieceColor.None, ChessPieceType.None), new Coordinate(newMove.To, -1, 0));
                        }
                        else
                        {
                            SetPieceAtCoordinate(new ChessPiece(ChessPieceColor.None, ChessPieceType.None), new Coordinate(newMove.To, 1, 0));
                        }
                    }
                    // Mark if the new move triggers the possibilty of an En Passant from the following turn.

                    int pawnDoubleFromRow = TurnColor == ChessPieceColor.White ? 1 : 6;
                    int pawnDoubleToRow = TurnColor == ChessPieceColor.White ? 3 : 4;
                    int enPassantTargetTargetRow = TurnColor == ChessPieceColor.White ? 2 : 5;
                    if (newMove.From.Row == pawnDoubleFromRow && newMove.To.Row == pawnDoubleToRow)
                    {
                        EnPassantTarget = new Coordinate(enPassantTargetTargetRow, newMove.From.Column);
                    }
                }
                // King movements disable castling.
                else if (newMove.PieceType == ChessPieceType.King)
                {
                    if (TurnColor == ChessPieceColor.White)
                    {
                        WhiteKingsideCastling = false;
                        WhiteQueensideCastling = false;
                    }
                    else
                    {
                        BlackKingsideCastling = false;
                        BlackQueensideCastling = false;
                    }
                }
                // Rook movements disable on their side.
                else if (newMove.PieceType == ChessPieceType.Rook)
                {
                    if (TurnColor == ChessPieceColor.White)
                    {
                        if (newMove.From.Equals(new Coordinate(FirstRowWhite, KingsideRookColumn)))
                        {
                            WhiteKingsideCastling = false;
                        }
                        else if (newMove.From.Equals(new Coordinate(FirstRowWhite, QueensideRookColumn)))
                        {
                            WhiteQueensideCastling = false;
                        }
                    }
                    else
                    {
                        if (newMove.From.Equals(new Coordinate(FirstRowBlack, KingsideRookColumn)))
                        {
                            BlackKingsideCastling = false;
                        }
                        else if (newMove.From.Equals(new Coordinate(FirstRowBlack, QueensideRookColumn)))
                        {
                            BlackQueensideCastling = false;
                        }
                    }
                }
                // Set square that the piece is moving from to empty, and moving to to have the piece.
                SetPieceAtCoordinate(new ChessPiece(ChessPieceColor.None, ChessPieceType.None), newMove.From);
                SetPieceAtCoordinate(new ChessPiece(TurnColor, newMove.IsPromotionToQueen ? ChessPieceType.Queen : newMove.PieceType), newMove.To);
            }

            // Reset or increment halfMove.
            if (newMove.IsCapture || newMove.PieceType == ChessPieceType.Pawn)
            {
                HalfMove = 0;
            }
            else
            {
                HalfMove++;
            }

            // Set applied move to be the previous move.
            PreviousMove = newMove;

            // Increment fullMove after blacks turn;
            if (TurnColor == ChessPieceColor.Black)
            {
                FullMove++;
            }

            // Switch turns.
            TurnColor = previousState.TurnColor == ChessPieceColor.White ? ChessPieceColor.Black : ChessPieceColor.White;

            bool isCheck = false;
            bool isCheckMate = false;
            if (lookForCheck)
            {
                new BoardState(this, out isCheck, out isCheckMate);
                PreviousMove = new ChessMove(PreviousMove.From, PreviousMove.To, PreviousMove.PieceType, PreviousMove.IsCapture, PreviousMove.IsPromotionToQueen, PreviousMove.DrawOfferExtended, isCheck, isCheckMate, PreviousMove.KingsideCastle, PreviousMove.QueensideCastle);
            }
            // Finally, determine the list of legal moves.
            InitializeAllPossibleMoves();
        }

        // Hypothetical board state where the turn color has not changed
        private BoardState(BoardState previousState, out bool isCheck, out bool isCheckMate)
        {
            BoardGrid = new ChessPiece[8, 8];
            // Copy elements.
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Coordinate coordinate = new Coordinate(row, column);
                    SetPieceAtCoordinate(previousState.GetPieceAtCoordinate(coordinate), coordinate);
                }
            }

            // Copy other board state values.
            BlackKingsideCastling = previousState.BlackKingsideCastling;
            BlackKingsideCastling = previousState.BlackQueensideCastling;
            WhiteKingsideCastling = previousState.WhiteKingsideCastling;
            WhiteQueensideCastling = previousState.WhiteQueensideCastling;
            TurnColor = previousState.TurnColor == ChessPieceColor.White ? ChessPieceColor.Black : ChessPieceColor.White; ;
            FullMove = previousState.FullMove;
            HalfMove = previousState.HalfMove;
            EnPassantTarget = new Coordinate(-1, -1);
            ParseLongAlgebraicNotation("");

            InitializeAllPossibleMoves();

            isCheck = KingIsCapturable();

            isCheckMate = false;
            if (isCheck)
            {
                this.TurnColor = TurnColor == ChessPieceColor.White ? ChessPieceColor.Black : ChessPieceColor.White;

                InitializeAllPossibleMoves();

                isCheckMate = true;
                for (int row = 0; row < 8 && isCheckMate; row++)
                {
                    for (int column = 0; column < 8 && isCheckMate; column++)
                    {
                        var moves = PossibleMovesGrid[row, column];
                        if (moves != null)
                        {
                            foreach (var move in moves)
                            {
                                if (!new BoardState(this, move, false).KingIsCapturable())
                                {
                                    isCheckMate = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void InitializeAllPossibleMoves()
        {
            PossibleMovesGrid = new HashSet<ChessMove>[8, 8];
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    PossibleMovesGrid[row, column] = InitializePossibleMoves(new Coordinate(row, column));
                }
            }
        }
        
        //TODO: move to server
        public HashSet<ChessMove> GetPossibleMoves(Coordinate coordinate)
        {
            return new HashSet<ChessMove>(PossibleMovesGrid[coordinate.Row, coordinate.Column]);
        }

        // Determine the current state of the board from Forsyth-Edwards Notation. Example FEN (initial board state):
        // rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1 
        private void ParseFEN(string fen)
        {
            // Break FEN into it's space-separated sections
            string[] sections = System.Text.RegularExpressions.Regex.Split(fen.Trim(), @"\s+");
            if (sections.Length != FenSectionCount)
            {
                throw new BoardStateException(String.Format("Invalid Forsyth-Edwards Notation '{0}'. Has {1} sections instead of {2}.", fen, sections.Length, FenSectionCount));
            }
            for (int i = 0; i < FenSectionCount; i++)
            {
                sections[i] = sections[i].Trim();
            }

            ParsePosistions(sections[0], fen);

            // Check whose turn it is
            if (sections[1].Equals("w"))
            {
                TurnColor = ChessPieceColor.White;
            }
            else if (sections[1].Equals("b"))
            {
                TurnColor = ChessPieceColor.Black;
            }
            else
            {
                throw new BoardStateException(String.Format("Invalid Forsyth-Edwards Notation '{0}'. Has color '{1}', instead of 'b' or 'w'.", fen, sections[1]));
            }

            ParseCastling(sections[2], fen);

            ParseEnPassantTarget(sections[3], fen);

            int halfMove;
            int fullMove;
            // Extract full move and half move integers
            if (!Int32.TryParse(sections[4], out halfMove))
            {
                throw new BoardStateException(String.Format("Invalid Forsyth-Edwards Notation '{0}'. Half move is '{1}' but must be an integer.", fen, sections[4]));
            }
            if (!Int32.TryParse(sections[5], out fullMove))
            {
                throw new BoardStateException(String.Format("Invalid Forsyth-Edwards Notation '{0}'. Full move is '{1}' but must be an integer.", fen, sections[5]));
            }
            HalfMove = halfMove;
            FullMove = fullMove;
        }
        
        //TODO: move to server - should be the main endpoint of communcation between client and server
        public BoardState TryApplyMove(ChessMove newMove, out bool putsUserInCheck)
        {
            var newBoardState = new BoardState(this, newMove);
            if (newBoardState.KingIsCapturable())
            {
                putsUserInCheck = true;
                return this;
            }
            else
            {
                putsUserInCheck = false;
                return newBoardState;
            }
        }

        // Check if the king capturable in this game state, which indicates an illegal gamestate. Used for checking legality of a hypothetical gamestate.
        public bool KingIsCapturable()
        {
            // Has to search through every piece and see if any of it's moves can capture a king.
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    foreach (ChessMove move in PossibleMovesGrid[row, column])
                    {
                        if (!move.KingsideCastle && !move.QueensideCastle && GetPieceAtCoordinate(move.To).Type == ChessPieceType.King)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // Determine legal moves for pieces that can move in specific directions until they reach the end of the board or another piece.
        private List<ChessMove> DetermineIndefiniteDirectionalMovements(List<int[]> directions, Coordinate fromCoordinate, ChessPiece fromPiece)
        {
            var legalMoves = new List<ChessMove>();
            Coordinate potentialCoordinate;
            foreach (int[] direction in directions)
            {
                int moved = 1;
                while (true)
                {
                    potentialCoordinate = new Coordinate(fromCoordinate, moved * direction[0], moved * direction[1]);
                    // If the potential coordinate does not go off bounds, allow a move there.
                    if (potentialCoordinate.IsInBoardBounds())
                    {
                        ChessPiece potentialCoordinatePiece = GetPieceAtCoordinate(potentialCoordinate);
                        bool blocked = potentialCoordinatePiece.Type != ChessPieceType.None;
                        bool capture = blocked && potentialCoordinatePiece.Color != TurnColor;
                        // Only move to blocked square if it is to be captured.
                        if (!blocked || capture)
                        {
                            // Check and checkmate values will only be verified after a move is chosen, to avoid the heavy processing for each move.
                            legalMoves.Add(new ChessMove(fromCoordinate, potentialCoordinate, fromPiece.Type, capture, false, false, false, false, false, false));
                        }
                        // Cannot move past another piece.
                        if (blocked) { break; }
                    }
                    else
                    {
                        break;
                    }
                    // Check further in this direction.
                    moved++;
                }
            }
            return legalMoves;
        }

        private HashSet<ChessMove> DetermineTranslationalMovements(List<int[]> tranlations, Coordinate fromCoordinate, ChessPiece fromPiece)
        {
            Coordinate potentialCoordinate;
            var legalMoves = new HashSet<ChessMove>();
            foreach (int[] translation in tranlations)
            {
                potentialCoordinate = new Coordinate(fromCoordinate, translation[0], translation[1]);
                if (potentialCoordinate.IsInBoardBounds())
                {
                    ChessPiece potentialCoordinatePiece = GetPieceAtCoordinate(potentialCoordinate);
                    bool blocked = potentialCoordinatePiece.Type != ChessPieceType.None;
                    bool capture = blocked && potentialCoordinatePiece.Color != TurnColor;
                    // Only move to blocked square if it is to be captured.
                    if (!blocked || capture)
                    {
                        // Check and checkmate values will only be verified after a move is chosen, to avoid the heavy processing for each move.
                        legalMoves.Add(new ChessMove(fromCoordinate, potentialCoordinate, fromPiece.Type, capture, false, false, false, false, false, false));
                    }
                }
            }
            return legalMoves;
        }

        // Note that this considers moving one's self into a checked position possible, even though it is not legal. This is to avoid the excessive computation of determining if this is the case. The check will be performed on tryApplyMove instead.
        private HashSet<ChessMove> InitializePossibleMoves(Coordinate fromCoordinate)
        {
            var legalMoves = new HashSet<ChessMove>();
            ChessPiece fromPiece = GetPieceAtCoordinate(fromCoordinate);
            // No valid moves for empty squares or if the color of the piece does not match whose turn it is.
            if (fromPiece.Type == ChessPieceType.None || fromPiece.Color == ChessPieceColor.None || fromPiece.Color != TurnColor)
            {
                // Empty at this point
                return legalMoves;
            }
            Coordinate potentialCoordinate;
            switch (fromPiece.Type)
            {
                case ChessPieceType.Rook:
                    legalMoves.UnionWith(DetermineIndefiniteDirectionalMovements(RookDirections, fromCoordinate, fromPiece));
                    break;

                case ChessPieceType.Knight:
                    legalMoves.UnionWith(DetermineTranslationalMovements(KnightTranslations, fromCoordinate, fromPiece));
                    break;

                case ChessPieceType.Bishop:
                    legalMoves.UnionWith(DetermineIndefiniteDirectionalMovements(BishopDirections, fromCoordinate, fromPiece));
                    break;

                case ChessPieceType.Queen:
                    legalMoves.UnionWith(DetermineIndefiniteDirectionalMovements(QueenDirectionsKingTranslations, fromCoordinate, fromPiece));
                    break;

                case ChessPieceType.King:
                    legalMoves.UnionWith(DetermineTranslationalMovements(QueenDirectionsKingTranslations, fromCoordinate, fromPiece));
                    if (TurnColor == ChessPieceColor.White)
                    {
                        if (WhiteKingsideCastling && BoardGrid[0, 5].Type == ChessPieceType.None && BoardGrid[0, 6].Type == ChessPieceType.None)
                        {
                            // Check and checkmate values will only be verified after a move is chosen, to avoid the heavy processing for each move.
                            legalMoves.Add(new ChessMove(fromCoordinate, new Coordinate(0, 6), ChessPieceType.King, false, false, false, false, false, true, false));
                        }
                        if (WhiteQueensideCastling && BoardGrid[0, 1].Type == ChessPieceType.None && BoardGrid[0, 2].Type == ChessPieceType.None && BoardGrid[0, 3].Type == ChessPieceType.None)
                        {
                            // Check and checkmate values will only be verified after a move is chosen, to avoid the heavy processing for each move.
                            legalMoves.Add(new ChessMove(fromCoordinate, new Coordinate(0, 2), ChessPieceType.King, false, false, false, false, false, false, true));
                        }
                    }
                    else
                    {
                        if (BlackKingsideCastling && BoardGrid[7, 5].Type == ChessPieceType.None && BoardGrid[7, 6].Type == ChessPieceType.None)
                        {
                            // Check and checkmate values will only be verified after a move is chosen, to avoid the heavy processing for each move.
                            legalMoves.Add(new ChessMove(fromCoordinate, new Coordinate(7, 6), ChessPieceType.King, false, false, false, false, false, true, false));
                        }
                        if (BlackQueensideCastling && BoardGrid[7, 1].Type == ChessPieceType.None && BoardGrid[7, 2].Type == ChessPieceType.None && BoardGrid[7, 3].Type == ChessPieceType.None)
                        {
                            legalMoves.Add(new ChessMove(fromCoordinate, new Coordinate(7, 2), ChessPieceType.King, false, false, false, false, false, false, true));
                        }
                    }
                    break;

                case ChessPieceType.Pawn:
                    // 1 if white, -1 if black.
                    int pawnDirection;
                    // How many steps foeward this pawn is allowed to move.
                    int pawnForwardAmountAllowed = 1;
                    if (TurnColor == ChessPieceColor.White)
                    {
                        pawnDirection = 1;
                        // Yet-to-move pawns can move forward two spaces.
                        if (fromCoordinate.Row == 1)
                        {
                            pawnForwardAmountAllowed = 2;
                        }
                    }
                    else
                    {
                        pawnDirection = -1;
                        // Yet-to-move pawns can move forward two spaces.
                        if (fromCoordinate.Row == 6)
                        {
                            pawnForwardAmountAllowed = 2;
                        }
                    }
                    int forwardMoved = 1;
                    // Continue checking the next forward step until the max amount of steps have been taken, or a piece has been encountered.
                    while (forwardMoved <= pawnForwardAmountAllowed)
                    {
                        potentialCoordinate = new Coordinate(fromCoordinate, forwardMoved * pawnDirection, 0);
                        // If the potential coordinate does not go off bounds and does not occupy the location of another piece, allow a move there
                        if (potentialCoordinate.IsInBoardBounds() && GetPieceAtCoordinate(potentialCoordinate).Type == ChessPieceType.None)
                        {
                            // If a white pawn moves to row 7 or a black pawn moves to row 0, a prootion is in order.
                            bool isPromotionToQueen = (potentialCoordinate.Row == (TurnColor == ChessPieceColor.White ? 7 : 0));
                            // Check and checkmate values will only be verified after a move is chosen, to avoid the heavy processing for each move.
                            legalMoves.Add(new ChessMove(fromCoordinate, potentialCoordinate, fromPiece.Type, false, isPromotionToQueen, false, false, false, false, false));
                        }
                        else
                        {
                            break;
                        }
                        forwardMoved++;
                    }
                    // Check pawn's ability to diagonally capture.
                    // Kingside Diagonal.
                    potentialCoordinate = new Coordinate(fromCoordinate, pawnDirection, 1);
                    if (potentialCoordinate.IsInBoardBounds())
                    {
                        ChessPiece pieceAtPotentialCoordinate = GetPieceAtCoordinate(potentialCoordinate);
                        if (pieceAtPotentialCoordinate.Type != ChessPieceType.None &&
                            TurnColor != pieceAtPotentialCoordinate.Color)
                        {
                            bool isPromotionToQueen = (potentialCoordinate.Row == (TurnColor == ChessPieceColor.White ? 7 : 0));
                            // Check and checkmate values will only be verified after a move is chosen, to avoid the heavy processing for each move.
                            legalMoves.Add(new ChessMove(fromCoordinate, potentialCoordinate, fromPiece.Type, true, isPromotionToQueen, false, false, false, false, false));
                        }
                        // Kingside Diagonal En Passant.
                        if (EnPassantTarget.IsInBoardBounds() && EnPassantTarget.Equals(potentialCoordinate))
                        {
                            bool isPromotionToQueen = (potentialCoordinate.Row == (TurnColor == ChessPieceColor.White ? 7 : 0));
                            // Check and checkmate values will only be verified after a move is chosen, to avoid the heavy processing for each move.
                            legalMoves.Add(new ChessMove(fromCoordinate, potentialCoordinate, fromPiece.Type, true, isPromotionToQueen, false, false, false, false, false));
                        }
                    }
                    // Queenside Diagonal.
                    potentialCoordinate = new Coordinate(fromCoordinate, pawnDirection, -1);
                    if (potentialCoordinate.IsInBoardBounds())
                    {
                        ChessPiece pieceAtPotentialCoordinate = GetPieceAtCoordinate(potentialCoordinate);
                        if (pieceAtPotentialCoordinate.Type != ChessPieceType.None &&
                            TurnColor != pieceAtPotentialCoordinate.Color)
                        {
                            bool isPromotionToQueen = (potentialCoordinate.Row == (TurnColor == ChessPieceColor.White ? 7 : 0));
                            // Check and checkmate values will only be verified after a move is chosen, to avoid the heavy processing for each move.
                            legalMoves.Add(new ChessMove(fromCoordinate, potentialCoordinate, fromPiece.Type, true, isPromotionToQueen, false, false, false, false, false));
                        }
                        // Queenside Diagonal En Passant.
                        if (EnPassantTarget.IsInBoardBounds() && EnPassantTarget.Equals(potentialCoordinate))
                        {
                            bool isPromotionToQueen = (potentialCoordinate.Row == (TurnColor == ChessPieceColor.White ? 7 : 0));
                            // Check and checkmate values will only be verified after a move is chosen, to avoid the heavy processing for each move.
                            legalMoves.Add(new ChessMove(fromCoordinate, potentialCoordinate, fromPiece.Type, true, isPromotionToQueen, false, false, false, false, false));
                        }
                    }
                    break;

                default:
                    // Already handled before switch
                    break;
            }

            return legalMoves;
        }

        // Determine what piece, if any, is on each square of the board by parsing the first
        // section of the FEN, for example: rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR
        private void ParsePosistions(string ranks, string fen)
        {
            // Row is rank - 1, for ease of array indexing (rank is a chess term -
            // the white queen and king are rank 1 in the initial game setup,
            // whereas the black queen and king are rank 8.
            int row = 7;
            // Column corresponds to file, but as integers starting at 0, for ease
            // of array indexing (file is a chess term - queens have file 'd' and
            // kings have file 'e' in the initial game setup.
            int column = 0;
            foreach (char c in ranks)
            {
                // Case that the character represents a chess piece.
                if (CharToChessPieceType.ContainsKey(Char.ToUpper(c)))
                {
                    if (column > 7)
                    {
                        throw new BoardStateException(string.Format("Invalid Forsyth-Edwards Notation '{0}'. More than 8 files in a rank.", fen));
                    }

                    ChessPieceColor color = Char.IsUpper(c) ? ChessPieceColor.White : ChessPieceColor.Black;
                    ChessPieceType type = CharToChessPieceType[Char.ToUpper(c)];
                    BoardGrid[row, column] = new ChessPiece(color, type);

                    column++;
                }
                // Case that the character represents some number of empty squares.
                else if (Char.IsNumber(c) && c != '0')
                {
                    column += Convert.ToInt32(Char.GetNumericValue(c));
                }
                // Case that the character represents moving to the next rank
                else if (c == '/')
                {
                    if (column != 8)
                    {
                        throw new BoardStateException(string.Format("Invalid Forsyth-Edwards Notation '{0}'. Less than 8 files in a rank.", fen));
                    }
                    row--;
                    column = 0;
                    if (row < 0)
                    {
                        throw new BoardStateException(string.Format("Invalid Forsyth-Edwards Notation '{0}'. More than 8 ranks.", fen));
                    }
                }
                else
                {
                    throw new BoardStateException(string.Format("Invalid Forsyth-Edwards Notation '{0}'. Invalid character '{1}' in piece-positions secion.", fen, c));
                }
            }
            if (row != 0 || column != 8)
            {
                throw new BoardStateException(string.Format("Invalid Forsyth-Edwards Notation '{0}'. Does not describe every square on board.", fen));
            }
        }

        // Determine the castling capability of each player using the castling section of the FEN, for example: KQkq.
        private void ParseCastling(string castling, string fen)
        {
            if (castling.Equals("-"))
            {
                WhiteKingsideCastling = false;
                WhiteQueensideCastling = false;
                BlackKingsideCastling = false;
                BlackQueensideCastling = false;
            }
            else
            {
                WhiteKingsideCastling = castling.Contains("K");
                WhiteQueensideCastling = castling.Contains("Q");
                BlackKingsideCastling = castling.Contains("k");
                BlackQueensideCastling = castling.Contains("q");

                // Not 100% necessary check but could be useful for catching bugs. If there are no castling opportunities, it should be marked with a "-".
                if (!(WhiteKingsideCastling || WhiteQueensideCastling || BlackKingsideCastling || BlackQueensideCastling))
                {
                    throw new BoardStateException(string.Format("Invalid Forsyth-Edwards Notation '{0}'. Castling section is invalid.", fen));
                }
            }
        }

        // Determine the potential target of an "en passant" move using the en passant section of the FEN, for example: c3.
        private void ParseEnPassantTarget(string enPassantTargetString, string fen)
        {
            if (enPassantTargetString.Equals("-"))
            {
                EnPassantTarget = new Coordinate(-1, -1);
            }
            else if (enPassantTargetString.Length != 2)
            {
                throw new BoardStateException(string.Format("Invalid Forsyth-Edwards Notation '{0}'. En Passant Target is not 2 characters.", fen));
            }
            else if (FileToColumn.ContainsKey(enPassantTargetString[0]) && Char.IsNumber(enPassantTargetString[1]) && enPassantTargetString[1] != '0' && enPassantTargetString[1] != '9')
            {
                EnPassantTarget = new Coordinate(FileToColumn[enPassantTargetString[0]], Convert.ToInt32(Char.GetNumericValue(enPassantTargetString[1])) - 1);
            }
            else
            {
                throw new BoardStateException(string.Format("Invalid Forsyth-Edwards Notation '{0}'. Invalid En Passant Target.", fen));
            }
        }

        // Determine what the previous move was using Long Algebraic Notation, for example: Rd3xd7Q.
        private void ParseLongAlgebraicNotation(string moveString)
        {
            moveString = moveString.Trim();
            int rowFrom, columnFrom, rowTo, columnTo;
            ChessPieceType movePieceType;
            bool isCapture = false, isPromotionToQueen = false, drawOfferExtended = false, isCheck = false, isCheckMate = false;

            int i = 0;
            int minLength;

            bool moveIsKingsideCastle = false;
            bool moveIsQueensideCastle = false;

            // Special case: no moves yet.
            if (moveString.Length == 0)
            {
                PreviousMove = new ChessMove(new Coordinate(-1, -1), new Coordinate(-1, -1), ChessPieceType.None, false, false, false, false, false, false, false);
                return;
            }

            // Special case: castling moves. Must be done in this order because QUEENSIDE_CASTLING_STRING ("0-0-0") starts with KINGSIDE_CASTLING_STRING("0-0").
            if (moveString.StartsWith(QueensideCastlingString))
            {
                movePieceType = ChessPieceType.King;
                moveIsQueensideCastle = true;
                i += QueensideCastlingString.Length;
                // Set to invalid values for special case of castling.
                rowFrom = -1; columnFrom = -1; rowTo = -1; columnTo = -1;
            }
            else if (moveString.StartsWith(KingsideCastlingString))
            {
                movePieceType = ChessPieceType.King;
                moveIsKingsideCastle = true;
                i += KingsideCastlingString.Length;
                // Set to invalid values for special case of castling.
                rowFrom = -1; columnFrom = -1; rowTo = -1; columnTo = -1;
            }
            else
            {

                // Check to see if the first character refers to the type of piece. If it is not, it is implicity a pawn.
                if (moveString.Length > 0 && CharToChessPieceType.ContainsKey(moveString[i]))
                {
                    movePieceType = CharToChessPieceType[moveString[i]];
                    i++;
                    // How many characters to inspect depends on whether or not the piece type was implicitly a pawn
                    minLength = MinimumAlgebraicNotationLength + 1;
                }
                else
                {
                    movePieceType = ChessPieceType.Pawn;
                    minLength = MinimumAlgebraicNotationLength;
                }

                if (moveString.Length < minLength)
                {
                    throw new BoardStateException(string.Format("Invalid Long Algebraic Notation '{0}'. Long Algebraic Notation is too few characters to be valid.", moveString));
                }

                if (FileToColumn.ContainsKey(moveString[i]))
                {
                    columnFrom = FileToColumn[moveString[i]];
                    i++;
                }
                else
                {
                    throw new BoardStateException(string.Format("Invalid Long Algebraic Notation '{0}'. Ivalid file character '{1}'", moveString, moveString[i]));
                }
                if (Char.IsNumber(moveString[i]) && moveString[i] != '0' && moveString[i] != '9')
                {
                    rowFrom = Convert.ToInt32(Char.GetNumericValue(moveString[i])) - 1;
                    i++;
                }
                else
                {
                    throw new BoardStateException(string.Format("Invalid Long Algebraic Notation '{0}'. Ivalid rank character '{1}'", moveString, moveString[i]));
                }
                if (moveString[i] == '-')
                {
                    isCapture = false;
                    i++;
                }
                else if (moveString[i] == 'x')
                {
                    isCapture = true;
                    i++;
                }
                else
                {
                    throw new BoardStateException(string.Format("Invalid Long Algebraic Notation '{0}'. Ivalid capture character '{1}'", moveString, moveString[i]));
                }

                if (FileToColumn.ContainsKey(moveString[i]))
                {
                    columnTo = FileToColumn[moveString[i]];
                    i++;
                }
                else
                {
                    throw new BoardStateException(string.Format("Invalid Long Algebraic Notation '{0}'. Ivalid file character '{1}'", moveString, moveString[i]));
                }
                if (Char.IsNumber(moveString[i]) && moveString[i] != '0' && moveString[i] != '9')
                {
                    rowTo = Convert.ToInt32(Char.GetNumericValue(moveString[i])) - 1;
                    i++;
                }
                else
                {
                    throw new BoardStateException(string.Format("Invalid Long Algebraic Notation '{0}'. Ivalid rank character '{1}'", moveString, moveString[i]));
                }
            }

            // Loop the rest of the characters, if any, which are flags that can be in any order
            while (i < moveString.Length)
            {
                if (moveString[i] == 'Q')
                {
                    isPromotionToQueen = true;
                }
                else if (moveString[i] == '=')
                {
                    drawOfferExtended = true;
                }
                else if (moveString[i] == '+')
                {
                    isCheck = true;
                }
                else if (moveString[i] == '#')
                {
                    isCheckMate = true;
                }
                else
                {
                    throw new BoardStateException(string.Format("Invalid Long Algebraic Notation '{0}'. Invalid flag character '{1}'", moveString, moveString[i]));
                }
                i++;
            }
            PreviousMove = new ChessMove(new Coordinate(rowFrom, columnFrom), new Coordinate(rowTo, columnTo), movePieceType, isCapture, isPromotionToQueen, drawOfferExtended, isCheck, isCheckMate, moveIsKingsideCastle, moveIsQueensideCastle);
        }

        public string ToForsythEdwardsNotation()
        {
            var fenBuilder = new StringBuilder();
            // Number of empty squares in a row. E.g. if a row has a white pawn, 6 empty squares, then a black queen, it will be "P6q".
            int repeatedEmptySquares = 0;
            // Fill in piece position section of fen
            for (int row = 7; row >= 0; row--)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (BoardGrid[row, column].Type == ChessPieceType.None)
                    {
                        repeatedEmptySquares++;
                    }
                    else
                    {
                        // Flush repeated empty squares
                        if (repeatedEmptySquares > 0)
                        {
                            fenBuilder.Append(repeatedEmptySquares.ToString());
                            repeatedEmptySquares = 0;
                        }
                        char c = ChessPieceToCapitalChar[(int)(BoardGrid[row, column].Type)];
                        // Use lower case for black pieces
                        fenBuilder.Append((BoardGrid[row, column].Color == ChessPieceColor.Black) ? Char.ToLower(c) : c);
                    }
                }
                // Flush repeated empty squares
                if (repeatedEmptySquares > 0)
                {
                    fenBuilder.Append(repeatedEmptySquares.ToString());
                    repeatedEmptySquares = 0;
                }
                // Last rank does not end with a slash, so instead add a space to move to the next setcion.
                fenBuilder.Append(row == 0 ? ' ' : '/');
            }

            // Who moves next, plus space to move to next section.
            fenBuilder.Append(TurnColor == ChessPieceColor.White ? "w " : "b ");

            // Castling section
            if (!(WhiteKingsideCastling || WhiteQueensideCastling || BlackKingsideCastling || BlackQueensideCastling))
            {
                fenBuilder.Append('-');
            }
            else
            {
                if (WhiteKingsideCastling)
                {
                    fenBuilder.Append('K');
                }
                if (WhiteQueensideCastling)
                {
                    fenBuilder.Append('Q');
                }
                if (BlackKingsideCastling)
                {
                    fenBuilder.Append('k');
                }
                if (BlackQueensideCastling)
                {
                    fenBuilder.Append('q');
                }
            }

            // En Passant Target, Half Move, and Full Move sections.
            fenBuilder.Append(' ');
            fenBuilder.Append(EnPassantTarget.ToString());
            fenBuilder.Append(' ');
            fenBuilder.Append(HalfMove.ToString());
            fenBuilder.Append(' ');
            fenBuilder.Append(FullMove.ToString());

            return fenBuilder.ToString();
        }

        public struct ChessPiece
        {
            private ChessPieceColor color;
            private ChessPieceType type;
            public ChessPieceColor Color
            {
                get { return color; }
            }
            public ChessPieceType Type
            {
                get { return type; }
            }
            public ChessPiece(ChessPieceColor color, ChessPieceType type)
            {
                this.color = color;
                this.type = type;
            }
        }

        public struct Coordinate
        {
            private int row;
            private int column;
            public int Row { get { return row; } }
            public int Column { get { return column; } }
            public Coordinate(int row, int column)
            {
                this.row = row;
                this.column = column;
            }
            // Make a new coordinate based on a translation of another coordinate
            public Coordinate(Coordinate originalCoordinate, int rowTranslation, int columnTranslation)
            {
                this.row = originalCoordinate.Row + rowTranslation;
                this.column = originalCoordinate.Column + columnTranslation;
            }
            public override string ToString()
            {
                return IsInBoardBounds() ? ColumnToFile[column] + (row + 1).ToString() : "-";
            }

            // Check if the coordinate is within the bounds of the board (i.e. not beyond the 8th row/column now before the 1st).
            public bool IsInBoardBounds()
            {
                return column >= 0 && column <= 7 && row >= 0 && row <= 7;
            }

            public override bool Equals(object obj)
            {
                return (obj is Coordinate) && ((Coordinate)obj).Row == row && ((Coordinate)obj).Column == column;
            }
        }

        public struct ChessMove
        {
            private Coordinate from;
            private Coordinate to;
            private ChessPieceType pieceType;
            private bool isCapture;
            private bool isPromotionToQueen;
            private bool drawOfferExtended;
            private bool isCheck;
            private bool isCheckMate;
            private bool kingsideCastle;
            private bool queensideCastle;
            public Coordinate From { get { return from; } }
            public Coordinate To { get { return to; } }
            public ChessPieceType PieceType { get { return pieceType; } }
            public bool IsCapture { get { return isCapture; } }
            public bool IsPromotionToQueen { get { return isPromotionToQueen; } }
            public bool DrawOfferExtended { get { return drawOfferExtended; } }
            public bool IsCheck { get { return isCheck; } }
            public bool IsCheckMate { get { return isCheckMate; } }
            public bool KingsideCastle { get { return kingsideCastle; } }
            public bool QueensideCastle { get { return queensideCastle; } }

            public ChessMove(Coordinate from, Coordinate to, ChessPieceType pieceType, bool isCapture, bool isPromotionToQueen, bool drawOfferExtended, bool isCheck, bool isCheckMate, bool kingsideCastle, bool queensideCastle)
            {
                this.from = from;
                this.to = to;
                this.pieceType = pieceType;
                this.isCapture = isCapture;
                this.isPromotionToQueen = isPromotionToQueen;
                this.drawOfferExtended = drawOfferExtended;
                this.isCheck = isCheck;
                this.isCheckMate = isCheckMate;
                this.kingsideCastle = kingsideCastle;
                this.queensideCastle = queensideCastle;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is ChessMove))
                {
                    return false;
                }
                var m = (ChessMove)obj;
                return m.From.Equals(from) &&
                    m.To.Equals(to) &&
                    m.PieceType == pieceType &&
                    m.IsCapture == isCapture &&
                    m.IsPromotionToQueen == isPromotionToQueen &&
                    m.DrawOfferExtended == drawOfferExtended &&
                    m.IsCheck == isCheck &&
                    m.IsCheckMate == isCheckMate &&
                    m.KingsideCastle == kingsideCastle &&
                    m.QueensideCastle == queensideCastle;
            }

            public string ToLongAlgebraicNotation()
            {

                string notation = "";
                if (kingsideCastle)
                {
                    notation += KingsideCastlingString;
                }
                else if (queensideCastle)
                {
                    notation += QueensideCastlingString;
                }
                else
                {
                    if (pieceType == ChessPieceType.None)
                    {
                        // If there is no piece type, there has not yet been a move.
                        return "";
                    }
                    // Pawn type is implied if no letter is shown
                    else if (pieceType != ChessPieceType.Pawn)
                    {
                        notation += ChessPieceToCapitalChar[(int)pieceType];
                    }
                    notation += from.ToString();
                    if (isCapture)
                    {
                        notation += 'x';
                    }
                    else
                    {
                        notation += '-';
                    }
                    notation += to.ToString();
                }
                if (isPromotionToQueen)
                {
                    notation += 'Q';
                }
                if (drawOfferExtended)
                {
                    notation += '=';
                }
                if (isCheck)
                {
                    notation += '+';
                }
                if (isCheckMate)
                {
                    notation += '#';
                }
                return notation;
            }
        }

        // Any Exceptions involving the Board State
        public class BoardStateException : System.Exception
        {
            public BoardStateException() : base() { }
            public BoardStateException(string message) : base(message) { }
            public BoardStateException(string message, System.Exception inner) : base(message, inner) { }
        }
        
        //TODO: ideally call server
        public ChessPiece GetPieceAtCoordinate(Coordinate coordinate)
        {
            return BoardGrid[coordinate.Row, coordinate.Column];
        }

        private void SetPieceAtCoordinate(ChessPiece piece, Coordinate coordinate)
        {
            BoardGrid[coordinate.Row, coordinate.Column] = piece;
        }
    }
}