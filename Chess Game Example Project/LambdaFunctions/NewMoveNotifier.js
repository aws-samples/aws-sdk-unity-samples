var AWS = require('aws-sdk');

console.log('Loading event');

// When we invoke this function, the parameters and context from our
// invocation are passed to this handler.
exports.handler = function(event, context) {

    var DDB_REGION = 'us-east-1';
    var SNS_REGION = 'us-east-1';
    var recordsProcessedCounter = event.Records.length;

    // No records to process    
    if (recordsProcessedCounter === 0) {
        context.succeed();
    }
    // Records to process.
    else {
        // Keeps track of how many of our records we have processed, and tells
        // Lambda that we have succeeded after we finish processing the last
        // record.
        var onRecordProcessed = function(error) {
            if (error) {
                console.log(error);
            }
            recordsProcessedCounter--;
            if (recordsProcessedCounter === 0) {
                context.succeed();
            }
        };

        // Process all records.
        for (i = 0; i < event.Records.length; i++) {
            record = event.Records[i];
            // We only care about insertions and modifications to the table.
            if (record.eventName != 'INSERT' && record.eventName != 'MODIFY') {
                onRecordProcessed(null);
                continue;
            }

            // Extract whose turn is next from the Forsyth Edwards Notation of
            // the board state, which we retrieve from the record.
            forsythEdwardsNotation = record.dynamodb.NewImage.FEN.S.trim();
            nextMoveIndicator = forsythEdwardsNotation.split(' ')[1];
            playerToNotify = '';
            if (nextMoveIndicator == 'w') {
                playerToNotify = record.dynamodb.NewImage.WhitePlayerId.S;
            } else if (nextMoveIndicator == 'b') {
                playerToNotify = record.dynamodb.NewImage.BlackPlayerId.S;
            }

            // Get the SNS endpoint ARNs for this player from our other DynamoDB
            // table, then publish a message to them. 
            if (playerToNotify) {
                var dynamodb = new AWS.DynamoDB({
                    region: DDB_REGION 
                });
                var params = {
                    TableName: 'SNSEndpointLookup',
                    KeyConditions: {
                        PlayerId: {
                            ComparisonOperator: 'EQ',
                            AttributeValueList: [{
                                'S': playerToNotify
                            }]
                        }
                    }
                };
                dynamodb.query(params, publishNotifications);
            } else {
                onRecordProcessed('Inavlid Forsyth Edwards Notation ' + forsythEdwardsNotation)
            }
        }
    }

    // Called when our query for SNS Endpoint ARNs succeeds.
    function publishNotifications(error, data) {
        if (error) {
            recordProcessed(error);
        } else {
            // Now that we have all of the endpoints of our player, let's notify them that 
            // it's their turn across all his devices.
            var sns = new AWS.SNS({
                region: SNS_REGION 
            });

            var publishedCounter = data.Items.length;

            // No endpoints available to publish to.
            if (publishedCounter === 0) {
                onRecordProcessed(null);
            }
            // Publish to all endpoints.
            else {
                // Keeps track of how many of the endpoints we have published
                // to, and calls onRecordProcessed when we have finished
                // publishing to all endpoints for the playerToNotify from
                // that record.
                var onPublished = function(error, data) {
                    if (error) {
                        console.log(data);
                    }
                    publishedCounter--;
                    if (publishedCounter === 0) {
                        onRecordProcessed(null);
                    }
                };

                // The notification message.
                var message = JSON.stringify({
                    default: 'It is now your turn in a chess match!',
                    APNS_SANDBOX: {
                        aps: {
                            alert: 'It is now your turn in a chess match!'
                        }
                    }
                });

                // Push to all endpoints.
                for (i = 0; i < data.Items.length; i++) {
                    var params = {
                        MessageStructure: 'json',
                        Message: message,
                        TargetArn: data.Items[i].SNSEndpointARN.S
                    };

                    sns.publish(params, onPublished);
                }
            }
        }
    }
};
