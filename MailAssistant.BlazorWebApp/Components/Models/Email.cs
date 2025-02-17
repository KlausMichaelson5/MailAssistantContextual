using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Data;
using System.ComponentModel.DataAnnotations;

namespace MailAssistant.BlazorWebApp.Components.Models
{
    public class Email
    {
#pragma warning disable SKEXP0001
        [VectorStoreRecordKey]
        [TextSearchResultLink]
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
        [TextSearchResultName]
        public string Subject { get; set; }

        [VectorStoreRecordData]
        [TextSearchResultValue]
        public string Body { get; set; }

        [VectorStoreRecordVector(Dimensions: 1536)]
        public ReadOnlyMemory<float>? Embedding { get; set; }

        public override string ToString()
        {
            string separator = "```\n";
            return $"{separator}From: {this.From}"
                + $"{separator}To: {this.To}"
                + $"{separator}Subject: {this.Subject}"
                + $"{separator}Date: {this.Date.ToString()}"
                + $"{separator}Body: {this.Body}";
        }
#pragma warning restore SKEXP0001
    }
}
