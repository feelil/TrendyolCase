using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrendyolCase.API;
using TrendyolCase.API.CrossCuttings;
using TrendyolCase.DataAccess;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        public string Get()
        {
            /* 
             
             docker build -f TrendyolCase.API/Dockerfile .. -t dockerapi
              docker run -p 8080:80 240b9d93192a              
             */
            using (var context = new TrendyolContext())
            {
                context.Database.EnsureCreated();// Database Creat If Doesn't exist.

                //
                // Create Smaple Data for Regex Patterns.                
                if (unitOfWork.RegexPatternpRepository.Get(x => x.Name == "ProductDetailAllParams").FirstOrDefault() == null)
                {
                    var pattern = new RegexPattern { Pattern = @"^https?:\/\/www\.?trendyol\.com\/([\w-]+)\/([\w-]+)-p-([0-9]+)\??(.*)", Name = "ProductDetailAllParams" };
                    unitOfWork.RegexPatternpRepository.Insert(pattern);
                    unitOfWork.Save();
                }
                if (unitOfWork.RegexPatternpRepository.Get(x => x.Name == "Search").FirstOrDefault() == null)
                {
                    var pattern = new RegexPattern { Pattern = @"^https?:\/\/www\.?trendyol\.com\/sr\?q=([^\n]+)", Name = "Search" };
                    unitOfWork.RegexPatternpRepository.Insert(pattern);
                    unitOfWork.Save();
                }


            }
            return "Service Is Ready ...";
        }
        // Gelen URL daha önce eklenememiş ise ve Uygun pattern ile eşleşirse DB ye kayıt ekler.
        [ServiceFilter(typeof(PatternChecker))]
        [HttpGet]
        [Route("WebUrlToDeepLink")]
        public ApiResult WebUrlToDeepLink(string webUrl)
        {            
            
            var appResult = new ApiResult((int)HttpStatusCode.OK, "ty://?Page=Home");
            var row = unitOfWork.WebLinkDeepRepository.Get(x => x.WebLink == webUrl.Trim() && x.IsActive == true).FirstOrDefault();
            if (row != null)
            {                
                appResult = new ApiResult((int)HttpStatusCode.OK, row.DeepLink);
            }
             
            return appResult;



        }
        [HttpGet]
        [Route("DeepLinkToWebUrl")]
        public ApiResult DeepLinkToWebUrl(string deepLink)
        {
           
           
            var appResult = new ApiResult((int)HttpStatusCode.NoContent, null);
            var row = unitOfWork.WebLinkDeepRepository.Get(x => x.DeepLink == deepLink.Trim() && x.IsActive == true).FirstOrDefault();
            if (row != null)
            {                
                 appResult = new ApiResult((int)HttpStatusCode.OK, row.WebLink);
            }
            return appResult;
        }
    }
}
