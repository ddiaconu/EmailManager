using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace EmailManager.Controllers
{
    [Route("api/test")]
    public class JwtAuthTestController : Controller
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public JwtAuthTestController()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpGet]
        [Authorize(Policy = "AdminUser")]
        public IActionResult Get()
        {
            var response = new
            {
                made_it = "Welcome Diana!"
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }
    }
}