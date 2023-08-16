using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyolCase.API.CrossCuttings
{
    public class ApiException
    {
        public ApiException(int starusCode, string message = null, string details = null)
        {
            this.StarusCode = starusCode;
            this.Message = message;
            this.Detils = details;

        }
        public int StarusCode { get; set; }
        public string Message { get; set; }
        public string Detils { get; set; }
    }
}
