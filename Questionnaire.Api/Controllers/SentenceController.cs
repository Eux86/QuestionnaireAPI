using Questionnaire.Api.Models;
using Questionnaire.Api.Models.DTO;
using QuestionnaireDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        //[Route("api/Sentence/Create")]
        //[HttpPost]
        //public HttpResponseMessage Create(SentenceDTO sentence)
        //{
        //    var returnData = Transformers.Transform(_repo.Save(Transformers.Transform(sentence)));
        //    var response = Request.CreateResponse(HttpStatusCode.Created, returnData);
        //    return response;
        //}

        [Route("api/Sentence/CreateMany")]
        [HttpPost]
        public HttpResponseMessage CreateMany(SentenceDTO[] sentences)
        {
            //var newSentences = sentences.Select(Transformers.Transform);
            //var toReturn = _repo.Save(newSentences.ToList());
            //var dtoToReturn = toReturn.Select(Transformers.Transform).ToList();

            //var response = Request.CreateResponse(HttpStatusCode.Created, dtoToReturn);
            var response = Request.CreateResponse(HttpStatusCode.Created, sentences);
            return response;
        }
    }
}