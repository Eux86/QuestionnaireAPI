using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuestionnaireDB.Repositories;
using Questionnaire.Api.Models.DTO;
using System.Configuration;

namespace Questionnaire.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserRepository _repo;

        public UserController()
        {
            _repo = new UserRepository();
        }

        [AllowAnonymous]
        public HttpResponseMessage Login(string username, string password)
        {
            var user = _repo.Get(username, password);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            } 
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }

        [AllowAnonymous]
        public HttpResponseMessage Register(string username, string password)
        {
            bool allowRegistration = ConfigurationManager.AppSettings["AllowRegistration"].ToLower()=="true";
            if (allowRegistration)
            {
                if (_repo.Add(username, password, 1))
                {
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
        }

    }
}
