using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrendyolCase.API.UrlParser;
using TrendyolCase.DataAccess;

namespace TrendyolCase.API.CrossCuttings
{
    public class PatternChecker : IAsyncActionFilter
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           
            if (context.ActionArguments.FirstOrDefault().Value != null)
            {
                var webUrl = context.ActionArguments.FirstOrDefault().Value.ToString();

                var row = unitOfWork.WebLinkDeepRepository.Get(x => x.WebLink == webUrl.Trim() && x.IsActive == true).FirstOrDefault();
                if (row == null)
                {
                    var allPatterns = unitOfWork.RegexPatternpRepository.Get();
                    var regexPattern = allPatterns.Where(x => Regex.IsMatch(webUrl, x.Pattern)).FirstOrDefault();
                    if (regexPattern != null)
                    {
                        string[] UrlData = Regex.Split(webUrl, regexPattern.Pattern,
                                                        RegexOptions.IgnoreCase,
                                                      TimeSpan.FromMilliseconds(500));
                        UrlData = UrlData.Where(x => x.Trim() != string.Empty).ToArray();

                        if (!string.IsNullOrEmpty(regexPattern.Name))
                        {
                            String fullName = $"TrendyolCase.API.UrlParser.{regexPattern.Name}";
                            Assembly assem = Assembly.GetExecutingAssembly();
                            dynamic myObject = assem.CreateInstance(fullName);
                            myObject.Execute(UrlData, webUrl.Trim());// İlgili RegEx'e ait olan class çağırılıyor.

                            
                        }
                        


                    }
                    else
                    {
                        String fullName = $"TrendyolCase.API.UrlParser.DefaultParser";// Varsayılan Parser
                        Assembly assem = Assembly.GetExecutingAssembly();
                        dynamic myObject = assem.CreateInstance(fullName);
                        myObject.Execute(null, webUrl.Trim());// İlgili RegEx'e ait olan class çağırılıyor.
                    }
                }
                
                   
               
                
            }

            await next();
        }
    }
}
