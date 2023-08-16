using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrendyolCase.DataAccess;

namespace TrendyolCase.API.UrlParser
{
    public class ProductDetailAllParams : IUrlParser
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public void Execute(string[] urlData, string webLink)
        {
            WebLinkDeepLink row = new WebLinkDeepLink();
            
            int length = urlData.Length;
            bool hasExtraParams = urlData[length - 1].IndexOf("&") > -1;
            string deepLink = $"ty://?Page=Product&ContentId={urlData[2]}";
            if (hasExtraParams)
            {
                string[] extraParams = urlData[length - 1].Split('&');

                int extraLength = extraParams.Length;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < extraParams.Length; i++)
                {

                    sb.Append("&");
                    var keyValue = extraParams[i].Split('=');
                    sb.Append(keyValue[0] + "=" + keyValue[1]);


                }
                deepLink = deepLink + sb.ToString();
            }           
           
            Regex.Replace(deepLink,"BoutiqueId", "CampaignId");
            row.IsActive = true;
            row.WebLink = webLink;
            row.ContentId = urlData[2];
            row.DeepLink = deepLink;
            unitOfWork.WebLinkDeepRepository.Insert(row);
            unitOfWork.Save();
        }
    }
}
