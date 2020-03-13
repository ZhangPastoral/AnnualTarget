using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspCoreTry.Models;
using log4net;

namespace AspCoreTry.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILog log=LogManager.GetLogger(Log4NetConfig.RepositoryName,"Home");


        public IActionResult Index()
        {
            log.DebugFormat("这是一个debug:{0}",DateTime.Now);
            return View();
        }

        public IActionResult Privacy()
        {
            throw new Exception("这是一个测试异常");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
