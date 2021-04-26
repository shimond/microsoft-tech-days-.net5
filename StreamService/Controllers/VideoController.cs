using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StreamService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<VideoController> _logger;

        public VideoController(ILogger<VideoController> logger)
        {
            _logger = logger;
        }


        [HttpGet("{movieName}")]
        public ActionResult Stream(string movieName)
        {
            _logger.LogInformation($"Movie stream stared {movieName}");
            string root = Directory.GetCurrentDirectory();
            string path = Path.Combine(root, "wwwroot", "Videos", movieName+ ".mp4");
            return PhysicalFile(path, "application/octet-stream", true);
        }


    }
}
