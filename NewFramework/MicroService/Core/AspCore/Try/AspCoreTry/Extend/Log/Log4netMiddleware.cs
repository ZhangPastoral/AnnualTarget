using System;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public class Log4netMiddleware
    {
         private static readonly ILog Log = LogManager.GetLogger(Log4NetConfig.RepositoryName,"anything");
        public readonly RequestDelegate Next;

        public Log4netMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception e)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status200OK;

                Log.ErrorFormat("捕捉到全局错误:{0}",e);

                if (context.Request.IsAjax())
                {
                    context.Response.ContentType = ResponseContentTypesEnum.Json.ToString();
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new { Code = 500, Message = e.Message }));
                }
                else
                {
                    context.Response.Redirect("/Home/Error");
                }
            }
        }
    }