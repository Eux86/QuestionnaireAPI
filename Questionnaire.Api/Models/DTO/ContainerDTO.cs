using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionnaireDB;

namespace Questionnaire.Api.Models.DTO
{
    public class ContainerDTO
    {
        public int Id { get; set; }
        public int QuestionSentenceId { get; set; }
        public int RightAnswerId { get; set; }
        public int IsRightAnswered { get; set; }
        public int SectionId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnswerDTO> Answer { get; set; }
        public virtual SentenceDTO Sentence { get; set; }
    }
}