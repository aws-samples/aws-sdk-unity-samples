using Amazon.Runtime;

namespace Amazon.CognitoSync.SyncManager.Storage.Model
{
    public class ListUpdatesResponse : AmazonCognitoResponse
    {
        private RemoteDataStorage.DatasetUpdates _datasetUpdates;

        public RemoteDataStorage.DatasetUpdates DatasetUpdates
        {
            get { return this._datasetUpdates; }
            set { this._datasetUpdates = value; }
        }
    }
}
