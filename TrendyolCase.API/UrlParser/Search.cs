using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyolCase.DataAccess;

namespace TrendyolCase.API.UrlParser
{
    public class Search : IUrlParser
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public void Execute(string[] urlData, string webLink)
        {
            WebLinkDeepLink row = new WebLinkDeepLink();
           
            string deepLink = $"ty://?Page=Search&Query={urlData[0]}";
            
            row.IsActive = true;
            row.WebLink = webLink;
            row.ContentId = urlData[0];
            row.DeepLink = deepLink;
            unitOfWork.WebLinkDeepRepository.Insert(row);
            unitOfWork.Save();
        }
    }
}
