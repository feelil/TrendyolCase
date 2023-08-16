using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyolCase.DataAccess;

namespace TrendyolCase.API.UrlParser
{
    interface IUrlParser
    {
        public void Execute(string[] urlData, string webLink);

    }
}
