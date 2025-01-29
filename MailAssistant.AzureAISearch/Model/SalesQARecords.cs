using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Data;

namespace MailAssistant.AzureAISearch.Model
{
    public class SalesQARecords
    {
        #pragma warning disable SKEXP0001 
        [VectorStoreRecordKey]
        [TextSearchResultName]
        public string id { get; set; } = Guid.NewGuid().ToString();

        [VectorStoreRecordData(IsFilterable = true)]
        [TextSearchResultValue]
        #pragma warning restore SKEXP0001
        public string SalesQandA { get; set; }= string.Empty;

        [VectorStoreRecordVector(Dimensions: 1536)]
        public ReadOnlyMemory<float>? SalesQandAEmbedding { get; set; }
    }
}