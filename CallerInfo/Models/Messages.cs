using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CallerInfo.Models
{
    public class Messages
    {
        public int MessageID { get; set; }

        public string Message { get; set; }

        public string EmptyMessage { get; set; }

        public DateTime MessageDate { get; set; }

        public string ManagerEmail { get; set; }
        public string LimitHardOrSoft { get; set; }
        public string LimitType { get; set; }
        public string LimitCurrencyCode { get; set; }
        public string LimitValue { get; set; }
        public Boolean LimitOverride { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }

    }
}
