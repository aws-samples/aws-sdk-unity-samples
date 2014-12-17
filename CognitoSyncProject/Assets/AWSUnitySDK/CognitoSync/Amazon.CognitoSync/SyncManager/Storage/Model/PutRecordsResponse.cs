using System.Collections.Generic;
using Amazon.Runtime;

namespace Amazon.CognitoSync.SyncManager.Storage.Model
{
    public class PutRecordsResponse : AmazonCognitoResponse
    {
        private List<Record> _updatedRecords;

        public List<Record> UpdatedRecords
        {
            get { return _updatedRecords; }
            set
            {
                this._updatedRecords = value;
            }
        }
    }
}
