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

        public HttpResponseMessage Get(int id)
        {
            var content = Transformers.Transform(_repo.Get(id));
            return Request.CreateResponse(HttpStatusCode.Found,content);
        }

        public SentenceDTO[] GetAll()
        {
            return _repo.GetAll().Select(Transformers.Transform).ToArray();
        }

        //[Route("api/Sentence/Create")]
        //[HttpPost]
        //public HttpResponseMessage Create(SentenceDTO sentence)
        //{
        //    var returnData = Transformers.Transform(_repo.UpdateQuestionnaire(Transformers.Transform(sentence)));
        //    var response = Request.CreateResponse(HttpStatusCode.Created, returnData);
        //    return response;
        //}

        [HttpPost]
        public HttpResponseMessage CreateMany(SentenceDTO[] sentences)
        {
            SentenceDTO[] returnData = sentences.Select(s => Transformers.Transform(_repo.Save(Transformers.Transform(s)))).ToArray();
            var response = Request.CreateResponse(HttpStatusCode.Created, returnData);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage Delete(SentenceDTO[] sentences)
        {
            if (sentences == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            else
            {
                List<SentenceDTO> returnData = new List<SentenceDTO>();
                foreach (var sentence in sentences)
                {
                    returnData.Add(Transformers.Transform(_repo.Delete(Transformers.Transform(sentence))));
                }
                var response = Request.CreateResponse(HttpStatusCode.Created, returnData);
                return response;
            }
        }

        [Route("api/sentence/GetByText")]
        public HttpResponseMessage GetByText(String text)
        {
            var filteredSentences = _repo.GetByText(text).Select(Transformers.Transform).ToArray();
            var response = Request.CreateResponse(HttpStatusCode.OK, filteredSentences);
            return response;
        }
    }
}