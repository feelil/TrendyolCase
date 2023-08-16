using System;
using System.Collections.Generic;
using System.Text;

namespace TrendyolCase.DataAccess
{
    public class WebLinkDeepLink
    {
        public int ID { get; set; }
        public string WebLink { get; set; }
        public string DeepLink { get; set; }
        public string ContentId { get; set; }

        public string BoutiqueId { get; set; }

        public string MerchantId { get; set; }
        public bool IsActive { get; set; }

    }
}
