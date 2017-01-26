using Questionnaire.Api.Models;
using Questionnaire.Api.Models.DTO;
using QuestionnaireDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Questionnaire.Api.Controllers
{
    public class SentenceController : ApiController
    {
        private readonly SentenceRepository _repo;

        public SentenceController()
        {
            _repo = new SentenceRepository();
        }

        public SentenceDTO[] Get()
        {
            return _repo.GetAll().Select(Transformers.Transform).ToArray();
        }
    }
}