﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
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

        public QuestionnaireDTO Get(int id)
        {
            var q = _repo.Get(id);
            return Transformers.Transform(q);
        }

        public QuestionnaireDTO[] GetAll(bool full = true)
        {
            if (full)
                return _repo.GetAllFull().Select(Transformers.Transform).ToArray();
            else 
                return _repo.GetAll().Select(Transformers.Transform).ToArray();
        }

        public HttpResponseMessage Create(QuestionnaireDTO questionnaireDto)
        {
            if (questionnaireDto == null) return new HttpResponseMessage(HttpStatusCode.BadRequest);

            var questionnaire = Transformers.Transform(questionnaireDto);
            HttpResponseMessage response = null;
            var savedQuestionnaire = _repo.Save(questionnaire);
            if (savedQuestionnaire!=null)
            {
                response = Request.CreateResponse(HttpStatusCode.Created, Transformers.Transform(savedQuestionnaire));
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotModified);
            }
            return response;
        }

        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {
            int numId = -1;
            int.TryParse(id, out numId);
            if (string.IsNullOrEmpty(id) || numId<0 || id=="undefined")
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage response = null;
            if (_repo.Delete(numId))
            {
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotModified);
            }
            return response;
        }
    }
}
