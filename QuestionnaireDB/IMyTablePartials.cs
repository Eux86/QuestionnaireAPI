using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB
{
    public interface  IMyTable
    {
        int Id { get; set; }
        bool Deleted { get; set; }
        void Delete(QuestionnaireDBContext context);
    }

    partial class Answer : IMyTable
    {
        public bool Deleted { get; set; }
        public void Delete(QuestionnaireDBContext context)
        {
            context.Entry(this).State = EntityState.Deleted;
        }
    }
    partial class Container : IMyTable
    {
        public bool Deleted { get; set; }
        public void Delete(QuestionnaireDBContext context)
        {
            context.Entry(this).State = EntityState.Deleted;
            foreach (var toDelete in context.Answer.Where(x => x.ContainerID == this.Id)){
                toDelete.Delete(context);
            }
        }
    }
    partial class Questionnaire : IMyTable
    {
        public bool Deleted { get; set; }
        public void Delete(QuestionnaireDBContext context)
        {
            context.Entry(this).State = EntityState.Deleted;
            foreach (var toDelete in context.Section.Where(x => x.QuestionnaireId == this.Id))
            {
                toDelete.Delete(context);
            }
        }
    }
    partial class Section : IMyTable
    {
        public bool Deleted { get; set; }
        public void Delete(QuestionnaireDBContext context)
        {
            context.Entry(this).State = EntityState.Deleted;
            foreach (var toDelete in context.Container.Where(x => x.SectionId == this.Id))
            {
                toDelete.Delete(context);
            }
        }
    }
    partial class Sentence : IMyTable
    {
        public bool Deleted { get; set; }
        public void Delete(QuestionnaireDBContext context)
        {
            context.Entry(this).State = EntityState.Deleted;
        }
    }
}
