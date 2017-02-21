using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireDB;

namespace Questionnaire.Api.Models.DTO
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public int SentenceId { get; set; }
        public int Selected { get; set; }
        public int ContainerId { get; set; }
        public bool IsCorrect { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateDate { get; set; }
        
        public virtual SentenceDTO Sentence { get; set; }
    }
}