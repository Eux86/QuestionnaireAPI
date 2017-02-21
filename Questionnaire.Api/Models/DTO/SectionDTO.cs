using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Api.Models.DTO
{
    public class SectionDTO
    {
        
        public int Id { get; set; }
        public string Description { get; set; }
        public int QuestionnaireId { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateDate { get; set; }

        public List<QuestionDTO> Questions { get; set; }
    }
}