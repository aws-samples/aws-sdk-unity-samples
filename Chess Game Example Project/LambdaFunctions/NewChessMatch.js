console.log('Loading function');

var TABLE_NAME = 'ChessMatches';
var INITIAL_FEN = 'rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1';
var INITIAL_LONG_ALG_NOTATION = ' ';

var doc = require('dynamodb-doc');
var dynamo = new doc.DynamoDB();

function checkMatchId(matchIdPrefix, counter, context, onUniqueFound) {
    var matchId = matchIdPrefix + counter;

    var params = {
        'TableName': TABLE_NAME,
        'Key': {
            'MatchId': matchId
        }
    }

    dynamo.getItem(params, function(err, data) {
        if (err) {
            context.fail(err);
        } else {
            // If the key exists, try again. This is an easy but inefficient
            // way of getting a unique ID. For the scope of the sample, this
            // is good enough, but if you are putting a game into production,
            // make appropriate changes for you use case!
            if (Object.keys(data).length > 0) {
                checkMatchId(matchIdPrefix, counter + 1, context, onUniqueFound);
            } else {
                onUniqueFound(matchId);
            }
        }
    });
}

// When we invoke this function, the parameters and context from our
// invocation are passed to this handler.
exports.handler = function(event, context) {
    // We expect the invoker to provide parameters with these names.
    var requesterId = event.requesterId;
    var opponentId = event.opponentId;

    if (!requesterId || !opponentId) {
        context.fail('Must provide requesterId and opponentId');
    }

    var blackPlayerId;
    var whitePlayerId;

    // Randomly choose which player is which chess piece color
    if (Math.random() < 0.5) {
        blackPlayerId = requesterId;
        whitePlayerId = opponentId;
    } else {
        blackPlayerId = opponentId;
        whitePlayerId = requesterId;
    }

    var matchIdPrefix = blackPlayerId + whitePlayerId;

    // Called when we find a match id to create the match with.
    var onUniqueFound = function(matchId) {
        // Add the match to the table.
        dynamo.putItem({
                'TableName': TABLE_NAME,
                'Item': {
                    'MatchId': matchId,
                    'BlackPlayerId': blackPlayerId,
                    'WhitePlayerId': whitePlayerId,
                    'FEN': INITIAL_FEN,
                    'AlgebraicNotation': INITIAL_LONG_ALG_NOTATION
                }
            },
            function(err, data) {
                if (err) {
                    context.fail(err);
                } else {
                    context.succeed(matchId);
                }
            }
        );
    }

    checkMatchId(matchIdPrefix, 0, context, onUniqueFound);
};
