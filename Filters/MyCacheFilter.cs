using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace pokelist.Filters
{

    public class MyCacheFilter : TypeFilterAttribute
    {

        public MyCacheFilter() : base(typeof(MyCacheFilterImpl))
        {
        }

        #region snippet_ActionFilter
        class MyCacheFilterImpl : IAsyncActionFilter
        { 
            IDistributedCache _cache;

            public MyCacheFilterImpl(IDistributedCache cache)
            {
                this._cache = cache;
            }

            public async Task OnActionExecutionAsync(
                ActionExecutingContext context,
                ActionExecutionDelegate next)
            {
            
                var cacheKey = GetTheCacheKey(context);


                Console.WriteLine($"before action executes {cacheKey}");

                var cachedString = await _cache.GetStringAsync(cacheKey);

                if (!string.IsNullOrEmpty(cachedString)) 
                {
                    //todo short circuit and break next execution by getting content from _cache
                    Console.WriteLine($"after action executes {cacheKey} - short circuit");
                    var cachedObj =  JsonConvert.DeserializeObject(cachedString);
                    context.Result = new Microsoft.AspNetCore.Mvc.ObjectResult(cachedObj);
                } 
                else 
                {
                    // do something before the action executes
                    var resultContext = await next();
                   
                    var objectResult = (resultContext.Result as Microsoft.AspNetCore.Mvc.ObjectResult);
                    
                    if (objectResult != null && objectResult.Value != null) 
                    {
                        var cacheOptions = new DistributedCacheEntryOptions();
                        cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                        var jsonSerializerSettings = new JsonSerializerSettings();
                        jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        var jsonString = JsonConvert.SerializeObject(objectResult.Value, jsonSerializerSettings);
                        await _cache.SetStringAsync(cacheKey, jsonString, cacheOptions);
                    }
                    
                    Console.WriteLine($"after action executes {cacheKey} ");
                   
                }
                
            }

            private string GetTheCacheKey(ActionExecutingContext context)
            {   
                var cacheKey = context.HttpContext.Request.Path.Value;
                return cacheKey;
            }
        }
        #endregion
    }
}