using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Questionnaire.Api.Models;
using Questionnaire.Api.Models.DTO;
using QuestionnaireDB;
using QuestionnaireDB.Repositories;

namespace Questionnaire.Api.Controllers
{
    public class QuestionnaireController : ApiController
    {
        private readonly QuestionnaireRepository _repo;

        public QuestionnaireController()
        {
            _repo = new QuestionnaireRepository();
        }

        public QuestionnaireDTO[] Get(bool full)
        {
            if (full)
                return _repo.GetAllFull().Select(Transformers.Transform).ToArray();
            else 
                return _repo.GetAll().Select(Transformers.Transform).ToArray();
        }

        public HttpResponseMessage Post(QuestionnaireDTO questionnaireDto)
        {
            if (questionnaireDto==null) return new HttpResponseMessage(HttpStatusCode.NoContent);

            var questionnaire = Transformers.Transform(questionnaireDto);
            HttpResponseMessage response = null;
            if (_repo.Add(questionnaire))
            {
                response = Request.CreateResponse(HttpStatusCode.Created, Transformers.Transform(questionnaire));
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotModified);
            }
            return response;
        }
    }
}
