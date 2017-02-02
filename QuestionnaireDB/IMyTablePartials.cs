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
        void Update(QuestionnaireDBContext context);
    }

    partial class Answer : IMyTable
    {
        public bool Deleted { get; set; }
        public void Delete(QuestionnaireDBContext context)
        {
            context.Entry(this).State = EntityState.Deleted;
        }
        public void Update(QuestionnaireDBContext context)
        {
            //context.Entry(this).State = EntityState.Modified;
            var inDb = context.Answer.Single(x => x.Id == this.Id);
            context.Entry(inDb).CurrentValues.SetValues(this);
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
        public void Update(QuestionnaireDBContext context)
        {
            var toRemove = Answer.Where(a => a.Deleted).ToList();
            foreach (var a in toRemove) Answer.Remove(a);
            //if (!context.Container.Local.Any(e => e.Id == this.Id)) 
            //    context.Entry(this).State = EntityState.Modified;
            var inDb = context.Container.Single(x => x.Id == this.Id);
            context.Entry(inDb).CurrentValues.SetValues(this);
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
        public void Update(QuestionnaireDBContext context)
        {
            var toRemove = Section.Where(s => s.Deleted).ToList();
            foreach (var s in toRemove) Section.Remove(s);
            //if (!context.Questionnaire.Local.Any(e => e.Id == this.Id))
            //    context.Entry(this).State = EntityState.Modified;
            var inDb = context.Questionnaire.Single(x => x.Id == this.Id);
            context.Entry(inDb).CurrentValues.SetValues(this);
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
        public void Update(QuestionnaireDBContext context)
        {
            var toRemove = Container.Where(c => c.Deleted).ToList();
            foreach (var c in toRemove) Container.Remove(c);
            //if (!context.Section.Local.Any(e => e.Id == this.Id))
            //    context.Entry(this).State = EntityState.Modified;
            var inDb = context.Section.Single(x => x.Id == this.Id);
            context.Entry(inDb).CurrentValues.SetValues(this);
        }
    }
    partial class Sentence : IMyTable
    {
        public bool Deleted { get; set; }
        public void Delete(QuestionnaireDBContext context)
        {
            context.Entry(this).State = EntityState.Deleted;
        }
        public void Update(QuestionnaireDBContext context)
        {
            //if (!context.Sentence.Local.Any(e => e.Id == this.Id))
            //    context.Entry(this).State = EntityState.Modified;
            var inDb = context.Sentence.Single(x => x.Id == this.Id);
            context.Entry(inDb).CurrentValues.SetValues(this);
        }
    }
}
