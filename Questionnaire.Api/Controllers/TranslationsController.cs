using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Questionnaire.Api.Controllers
{
    public class TranslationsController : ApiController
    {
        [Authorize]
        [HttpPost]
        public HttpResponseMessage DeleteList(KeyValuePair<string, string> translations)
        {
            return Request.CreateResponse(HttpStatusCode.OK,translations);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage UpdateList(KeyValuePair<string, string> translations)
        {
            return Request.CreateResponse(HttpStatusCode.OK, translations);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddList(KeyValuePair<string, string> translations)
        {
            return Request.CreateResponse(HttpStatusCode.OK, translations);
        }

        [Authorize]
        public HttpResponseMessage GetAll()
        {
            var translations = new List<KeyValuePair<string, string>>();
            translations.Add(new KeyValuePair<string, string>("test0", "translation test 0"));
            translations.Add(new KeyValuePair<string, string>("test1", "translation test 1"));
            translations.Add(new KeyValuePair<string, string>("test2", "translation test 2"));
            return Request.CreateResponse(HttpStatusCode.OK, translations);
        }

        [Authorize]
        public HttpResponseMessage GetVersion()
        {
            var version = DateTime.Now;
            return Request.CreateResponse(HttpStatusCode.OK, version);
        }
    }
}
