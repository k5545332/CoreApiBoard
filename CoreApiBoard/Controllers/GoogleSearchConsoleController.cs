using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Controllers
{
    [Route("googlec40442aeb226f494.html")]
    [ApiController]
    public class GoogleSearchConsoleController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public GoogleSearchConsoleController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet()]
        public IActionResult Get()
        {
            Byte[] b = System.IO.File.ReadAllBytes($@"googlec40442aeb226f494.html");
            return File(b, "text/html");
        }
    }
}
