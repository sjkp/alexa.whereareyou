using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexa.WhereAreYou.Controllers
{
    public class AlexaController : ApiController
    {
        [Route("alexa/whereareyou")]
        [HttpPost]
        public HttpResponseMessage SampleSession()
        {
            var speechlet = new WhereAreYouSpeechlet();
            return speechlet.GetResponse(Request);
        }
    }
}