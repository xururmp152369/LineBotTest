using LineBotTest1.Apps;
using LineBotTest1.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreLineBotSDK.Filters;
using NetCoreLineBotSDK.Interfaces;
using NetCoreLineBotSDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineBotTest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineController : ControllerBase
    {
        private readonly ILineMessageUtility lineMessageUtility;
        private readonly QnAMakerUtility qnAMakerUtility;
        public LineController(ILineMessageUtility _lineMessageUtility, QnAMakerUtility _qnAMakerUtility)
        {
            lineMessageUtility = _lineMessageUtility;
            qnAMakerUtility = _qnAMakerUtility;
        }

        [HttpPost]
        [LineVerifySignature]
        public async Task<IActionResult> Post(WebhookEvent request)
        {
            var app = new LineBotSampleApp(lineMessageUtility, qnAMakerUtility);
            await app.RunAsync(request.events);
            return Ok();
        }
    }
}
//https://linebottestapi.azurewebsites.net/api/line
//https://.ngrok.io/api/line