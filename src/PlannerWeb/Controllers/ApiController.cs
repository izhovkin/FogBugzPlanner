using FogBugzPlanner.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly ClientOptions _clientOptions;

        public ApiController(IOptions<ClientOptions> clientOptions)
        {
            _clientOptions = clientOptions.Value;
        }

        [HttpGet("cases")]
        public async Task<IActionResult> FindCases([FromQuery] string query)
        {
            var client = new FbClient(new Uri(_clientOptions.Uri))
            {
                Token = _clientOptions.Token
            };

            var cases = await client.SearchCases(query);

            return Json(cases);
        }
    }
}
