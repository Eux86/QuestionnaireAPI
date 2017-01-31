using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Api.Models.DTO
{
    public class QuestionnaireDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public List<SectionDTO> Sections { get; set; }
        public bool Deleted { get; set; }
    }
}