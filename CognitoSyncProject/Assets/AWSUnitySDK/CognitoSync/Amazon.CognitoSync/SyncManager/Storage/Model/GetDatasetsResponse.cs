using System.Collections.Generic;
using Amazon.Runtime;

namespace Amazon.CognitoSync.SyncManager.Storage.Model
{

    public class GetDatasetsResponse : AmazonCognitoResponse
    {
        private List<DatasetMetadata> _datasets;

        public List<DatasetMetadata> Datasets
        {
            get { return _datasets; }
            set { this._datasets = value; }
        }
    }
}
