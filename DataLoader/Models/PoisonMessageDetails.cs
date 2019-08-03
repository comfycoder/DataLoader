using System;
using System.Collections.Generic;
using System.Text;

namespace DataLoader.Models
{
    public class PoisonMessageDetails
    {
        public string RowKey { get; set; }

        public string PartitionKey { get; set; }

        public string OriginalMessageContent { get; set; }

        public string OriginalMessageId { get; set; }
    }
}
