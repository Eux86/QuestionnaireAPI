using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Questionnaire.Api.Models;
using Questionnaire.Api.Models.DTO;
using QuestionnaireDB.Repositories;

namespace Questionnaire.Api.Controllers
{
    public class LanguageController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            LanguageRepository rep = new LanguageRepository();
            try
            {
                var all = rep.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, all.Select(Transformers.Transform));
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Add(LanguageDTO language)
        {
            language.Active = false; // Don't allow to add a language as active, to avoid inchoerence
            LanguageRepository rep = new LanguageRepository();
            try
            {
                var deleted = rep.Add(Transformers.Transform(language));
                return Request.CreateResponse(HttpStatusCode.OK, Transformers.Transform(deleted));
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Delete(LanguageDTO language)
        {
            LanguageRepository rep = new LanguageRepository();
            try
            {
                var deleted = rep.Delete(Transformers.Transform(language));
                return Request.CreateResponse(HttpStatusCode.OK, Transformers.Transform(deleted));
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage SetActive(LanguageDTO language)
        {
            LanguageRepository rep = new LanguageRepository();
            try
            {
                var activated = rep.Activate(Transformers.Transform(language));
                return Request.CreateResponse(HttpStatusCode.OK, Transformers.Transform(activated));
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }


    }
}