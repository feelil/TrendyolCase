using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyolCase.DataAccess;

namespace TrendyolCase.API.UrlParser
{
    public class DefaultParser : IUrlParser
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public void Execute(string[] urlData, string webLink)
        {
            WebLinkDeepLink row = new WebLinkDeepLink();

            string deepLink = $"ty://?Page=Home";

            row.IsActive = true;
            row.WebLink = webLink;           
            row.DeepLink = deepLink;
            unitOfWork.WebLinkDeepRepository.Insert(row);
            unitOfWork.Save();
        }
    }
}
