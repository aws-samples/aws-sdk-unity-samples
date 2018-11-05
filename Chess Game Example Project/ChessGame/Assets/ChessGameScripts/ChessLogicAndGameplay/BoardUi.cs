using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace AWSSDK.Examples.ChessGame
{
    // Attached to a panel to display the current chess board and allow users to make moves.
    public class BoardUi : MonoBehaviour
    {
        public Text TitleText;
        public Text StatusText;

        public Image BoardSquarePrefab;
        public Image WhiteKingPrefab;
        public Image WhiteQueenPrefab;
        public Image WhiteBishopPrefab;
        public Image WhiteRookPrefab;
        public Image WhiteKnightPrefab;
        public Image WhitePawnPrefab;
        public Image BlackKingPrefab;
        public Image BlackQueenPrefab;
        public Image BlackBishopPrefab;
        public Image BlackRookPrefab;
        public Image BlackKnightPrefab;
        public Image BlackPawnPrefab;

        public List<GameObject> RankPanels;
        public GameState.MatchState CurrentMatchState { get; private set; }

        private Transform[,] _transformGrid;
        private Transform[,] TransformGrid
        {
            get
            {
                if (_transformGrid == null)
                {
                    _transformGrid = new Transform[8, 8];
                    for (int r = 0; r < 8; r++)
                    {
                        var rankPanel = RankPanels[r];
                        for (int c = 0; c < 8; c++)
                        {
                            TransformGrid[r, c] = rankPanel.transform.GetChild(c);
                            TransformGrid[r, c].GetComponent<Image>().color = (c + (r % 2)) % 2 == 1 ? Color.white : Color.black;
                        }
                    }
                }
                return _transformGrid;
            }
        }

        private GameManager.CurrentMatchStateAvailableHandler CurrentMatchStateAvailableHandler;

        void Start()
        {
            if (string.IsNullOrEmpty(TitleText.text))
            {
                TitleText.text = "Match Loading...";
            }
            if (string.IsNullOrEmpty(StatusText.text))
            {
                StatusText.text = "Match Loading...";
            }
        }

        void OnEnable()
        {
            AttachListener();
        }

        void OnDisable()
        {
            DetachListener();
            GameManager.Instance.SyncLocallySaved();
        }

        void AttachListener()
        {
            CurrentMatchStateAvailableHandler = delegate(string error, GameState.MatchState matchState)
            {
                if (string.IsNullOrEmpty(error))
                {
                    TitleText.text = string.Format("Vs. {0}", matchState.Opponent.Name);
                    RefreshBoard(matchState, null);
                }
                else
                {
                    StatusText.text = error;
                }
            };
            GameManager.Instance.RegisterOnCurrentMatchStateAvailableHandler(CurrentMatchStateAvailableHandler);
        }

        void DetachListener()
        {
            if (CurrentMatchStateAvailableHandler != null)
            {
                GameManager.Instance.UnregisterOnCurrentMatchStateAvailableHandler(CurrentMatchStateAvailableHandler);
            }
        }

        public Image prefabToPieceImage(BoardState.ChessPiece piece, Color color)
        {
            Image pieceSpritePrefab = null;
            switch (piece.Type)
            {
                case BoardState.ChessPieceType.Rook:
                    pieceSpritePrefab = piece.Color == BoardState.ChessPieceColor.White ? WhiteRookPrefab : BlackRookPrefab;
                    break;
                case BoardState.ChessPieceType.Knight:
                    pieceSpritePrefab = piece.Color == BoardState.ChessPieceColor.White ? WhiteKnightPrefab : BlackKnightPrefab;
                    break;
                case BoardState.ChessPieceType.Bishop:
                    pieceSpritePrefab = piece.Color == BoardState.ChessPieceColor.White ? WhiteBishopPrefab : BlackBishopPrefab;
                    break;
                case BoardState.ChessPieceType.Queen:
                    pieceSpritePrefab = piece.Color == BoardState.ChessPieceColor.White ? WhiteQueenPrefab : BlackQueenPrefab;
                    break;
                case BoardState.ChessPieceType.King:
                    pieceSpritePrefab = piece.Color == BoardState.ChessPieceColor.White ? WhiteKingPrefab : BlackKingPrefab;
                    break;
                case BoardState.ChessPieceType.Pawn:
                    pieceSpritePrefab = piece.Color == BoardState.ChessPieceColor.White ? WhitePawnPrefab : BlackPawnPrefab;
                    break;
            }

            var pieceImage = Instantiate(pieceSpritePrefab) as Image;
            float pieceWScale = 50 / pieceImage.rectTransform.rect.width;
            float pieceHScale = 50 / pieceImage.rectTransform.rect.height;
            pieceImage.rectTransform.localScale = new Vector3(pieceWScale, pieceHScale);
            pieceImage.rectTransform.localPosition = new Vector3();
            pieceImage.color = color;
            return pieceImage;
        }

        void RefreshBoard(GameState.MatchState matchState, BoardState.Coordinate? selected)
        {
            CurrentMatchState = matchState;
            StatusText.text = string.Format("{0} ({1})",
                matchState.IsSelfTurn() ? "Your Turn" : "Their Turn",
                matchState.BoardState.TurnColor == BoardState.ChessPieceColor.White ? "white" : "black");

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    foreach (Transform child in TransformGrid[r, c])
                    {
                        Destroy(child.gameObject);
                    }
                }
            }

            if (matchState.BoardState.PreviousMove.IsCheckMate)
            {
                TitleText.text = "Game Finished";
                StatusText.text = string.Format("{0} won!", matchState.IsSelfTurn() ? "They" : "You");
                return;
            }

            HashSet<BoardState.ChessMove> moves;
            var moveDestinations = new HashSet<BoardState.Coordinate>();
            if (selected.HasValue)
            {
                moves = matchState.BoardState.GetPossibleMoves(selected.Value);
                //TODO: server.GetPossibleMoves(board_id, selected.value)
            }
            else
            {
                moves = new HashSet<BoardState.ChessMove>();
            }
            foreach (var move in moves)
            {
                moveDestinations.Add(move.To);
            }

            // Fill in pieces, except those that are move destinations
            for (int c = 0; c < 8; c++)
            {
                for (int r = 0; r < 8; r++)
                {
                    var coordinate = new BoardState.Coordinate(r, c);
                    bool isSelectedCoordinate = selected.HasValue && selected.Value.Equals(coordinate);
                    bool isMoveDestination = moveDestinations.Contains(coordinate);

                    BoardState.ChessPiece piece = matchState.BoardState.GetPieceAtCoordinate(coordinate);
                    // pieces that are move destinations are handled later because clicking on them has movement behavior.
                    if (piece.Type != BoardState.ChessPieceType.None && !isMoveDestination)
                    {
                        Color pieceHighlight = isSelectedCoordinate ? Color.green : Color.white;
                        Image pieceImage = prefabToPieceImage(piece, pieceHighlight);
                        pieceImage.transform.SetParent(TransformGrid[r, c], false);
                        // Only allow player to make moves if it is their turn. Games against local
                        // player allow both players to make a move on the same device.
                        if (matchState.IsSelfTurn() || matchState.Opponent.IsLocalOpponent())
                        {
                            pieceImage.GetComponent<Button>().onClick.AddListener(delegate
                            {
                                if (selected == null)
                                {
                                    RefreshBoard(matchState, coordinate);
                                }
                                else if (selected.Value.Equals(coordinate))
                                {
                                    RefreshBoard(matchState, null);
                                }
                                else
                                {
                                    RefreshBoard(matchState, coordinate);
                                }
                            });
                        }
                    }
                }
            }

            // Fill in pieces that are move destinations
            foreach (var move in moves)
            {
                bool putsUserInCheck;
                var newMatchState = new GameState.MatchState(
                    matchState.Opponent,
                    matchState.BoardState.TryApplyMove(move, out putsUserInCheck),
                    //TODO: TryApplyMove(move, board_id)
                    matchState.SelfIsWhite,
                    matchState.Identifier);

                if (!putsUserInCheck)
                {
                    bool isOpponentPiece = matchState.BoardState.GetPieceAtCoordinate(move.To).Type != BoardState.ChessPieceType.None;
                    //TODO: GetPieceAtCoordinate(board_id, coord)
                    Color pieceHighlight = isOpponentPiece ? Color.red : Color.blue;
                    BoardState.ChessPiece fromPiece = matchState.BoardState.GetPieceAtCoordinate(move.From);
                    Image pieceImage = prefabToPieceImage(fromPiece, pieceHighlight);
                    pieceImage.transform.SetParent(TransformGrid[move.To.Row, move.To.Column], false);

                    // Only allow player to make moves if it is their turn. Games against local
                    // player allow both players to make a move on the same device.
                    if (matchState.IsSelfTurn() || matchState.Opponent.IsLocalOpponent())
                    {
                        pieceImage.GetComponent<Button>().onClick.AddListener(delegate
                        {
                            // GameManager will handler whether or not we are saving a local or multiplayer
                            // game, and will notify us of the updated state via our
                            // CurrentMatchStateAvailableHandler
                            StatusText.text = "Saving Match...";
                            GameManager.Instance.UpdateMatchState(newMatchState);
                        });
                    }
                }
            }
        }
    }
}
