using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireDB;

namespace Questionnaire.Api.Models.DTO
{
    public class SentenceDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateDate { get; set; }

    }
}