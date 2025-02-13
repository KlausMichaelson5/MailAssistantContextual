using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Data;

namespace MailAssistant.Helpers.Models
{
    public class Email
    {
        [VectorStoreRecordKey]
        public string Id { get; set; }

        [VectorStoreRecordData(IsFilterable = true)]
        [EmailAddress]
        public string From { get; set; } = string.Empty;

        [VectorStoreRecordData(IsFilterable = true)]
        [EmailAddress]
        public string To { get; set; } = string.Empty;

        [VectorStoreRecordData(IsFilterable = true)]
        public string Date { get; set; }

        [VectorStoreRecordData(IsFullTextSearchable = true)]
        #pragma warning disable SKEXP0001
        [TextSearchResultValue]
        #pragma warning restore SKEXP0001
        public string Subject { get; set; }

        [VectorStoreRecordData]
        #pragma warning disable SKEXP0001
        [TextSearchResultValue]
        #pragma warning restore SKEXP0001
        public string Body { get; set; }

        [VectorStoreRecordVector(Dimensions: 1536)]
        public ReadOnlyMemory<float>? Embedding { get; set; }
    }
}
