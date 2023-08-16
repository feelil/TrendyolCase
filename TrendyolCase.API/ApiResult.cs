using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyolCase.API
{
    public class ApiResult
    {
        public ApiResult(int starusCode,string message=null,string details=null)
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
