using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using QuestionnaireDB.Repositories;
using System.Net.Http.Headers;

namespace Questionnaire.Api.Controllers
{
    public class FileController : ApiController
    {
        int MAX_FILE_NUMBER = 1;

        [HttpPost]
        //[Authorize]
        public HttpResponseMessage Upload()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                string fileName = httpRequest.Files.Keys[0];
                var file = httpRequest.Files[fileName];
                var filePath = HttpContext.Current.Server.MapPath("~/App_Data/Images/" + file.FileName);
                file.SaveAs(filePath);

                FileRepository repo = new FileRepository();
                var savedFile = repo.Save(new QuestionnaireDB.File() { name = file.FileName, path = filePath });

                return Request.CreateResponse(HttpStatusCode.Created, Questionnaire.Api.Models.Transformers.Transform(savedFile));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Get(int id)
        {
            FileRepository repo = new FileRepository();
            var image = repo.Get(id);

            string fileName = string.Format("{0}.jpg", id);
            if (image==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            FileStream fileStream = new FileStream(image.path, FileMode.Open);
            HttpResponseMessage response = new HttpResponseMessage { Content = new StreamContent(fileStream) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            response.Content.Headers.ContentLength = fileStream.Length;
            return response;

        }
    }
}
