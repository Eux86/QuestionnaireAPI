using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Web.XmlTransform;
using Questionnaire.Api.Models;
using Questionnaire.Api.Models.DTO;
using QuestionnaireDB.Repositories;

namespace Questionnaire.Api.Controllers
{
    public class TranslationsController : ApiController
    {
        [Authorize]
        [HttpPost]
        public HttpResponseMessage DeleteList(List<TranslationDTO> translations)
        {
            TranslationRepository repo = new TranslationRepository();
            if (translations==null || !translations.Any() || repo.Delete(translations.Select(x => x.Id).ToArray()))
            {
                return Request.CreateResponse(HttpStatusCode.OK, translations);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, translations);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage UpdateList(List<TranslationDTO> translations)
        {
            TranslationRepository repo = new TranslationRepository();
            if (translations == null || !translations.Any() || repo.Update(translations.Select(Transformers.Transform)))
            {
                return Request.CreateResponse(HttpStatusCode.OK, translations);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, translations);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddList(List<TranslationDTO> translations)
        {
            TranslationRepository repo = new TranslationRepository();
            if (translations == null || !translations.Any() || repo.Add(translations.Select(Transformers.Transform)))
            {
                return Request.CreateResponse(HttpStatusCode.OK, translations);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, translations);
            }
        }

        [Authorize]
        public HttpResponseMessage GetAll()
        {
            TranslationRepository repo = new TranslationRepository();
            var all = repo.GetAll().Select(Transformers.Transform);
            return Request.CreateResponse(HttpStatusCode.OK, all);

            //var translations = new List<KeyValuePair<string, string>>();
            //translations.Add(new KeyValuePair<string, string>("test0", "translation test 0"));
            //translations.Add(new KeyValuePair<string, string>("test1", "translation test 1"));
            //translations.Add(new KeyValuePair<string, string>("test2", "translation test 2"));
            //return Request.CreateResponse(HttpStatusCode.OK, translations);
        }

        [Authorize]
        public HttpResponseMessage GetVersion()
        {
            TranslationRepository repo = new TranslationRepository();
            var version = repo.GetLatestVersion();
            return Request.CreateResponse(HttpStatusCode.OK, version);
        }
    }
}
