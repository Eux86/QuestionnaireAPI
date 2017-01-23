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

        public List<ContainerDTO> Container { get; set; }
    }
}