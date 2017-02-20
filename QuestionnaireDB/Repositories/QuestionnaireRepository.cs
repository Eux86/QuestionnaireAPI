using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB.Repositories
{
    public class QuestionnaireRepository
    {
        public Questionnaire UpdateQuestionnaire(Questionnaire questionnaire)
        {
            Questionnaire toReturn = null;
            using (var db = new QuestionnaireDBContext())
            {
                // Update children
                foreach (var section in questionnaire.Section)
                {
                    UpdateSection(db, section);
                }

                Questionnaire questInDb = db.Questionnaire.SingleOrDefault(x => x.Id == questionnaire.Id);

                // Check if deleted
                if (questionnaire.Deleted)
                {
                    if (questInDb != null)
                    {
                        db.Questionnaire.Remove(questInDb);
                    }
                    foreach (var section in questionnaire.Section)
                    {
                        section.Deleted = true;
                    }
                }
                else
                {
                    // Check if new or updated
                    if (questInDb == null)
                    {
                        db.Questionnaire.Add(questionnaire);
                    }
                    else
                    {
                        db.Entry(questInDb).CurrentValues.SetValues(questionnaire);
                    }
                }
                
                db.SaveChanges();
                toReturn = db.Questionnaire.OrderBy(q => q.Date).
                    Include(q => q.Section.Select(s => s.Container.Select(c => c.Answer.Select(a => a.Sentence))))
                    .Include(q => q.Section.Select(s => s.Container.Select(c => c.Sentence)))
                    .Single(x => x.Id == questionnaire.Id);

                ////var original = db.Questionnaire.Find(questionnaire.Id);
                //db.UpdateEntitiesState(questionnaire);
                //db.SaveChanges();
                //toReturn = questionnaire;
            }
            return toReturn;
        }

        public void UpdateSection(QuestionnaireDBContext db, Section section)
        {
            // Update children
            foreach (var container in section.Container)
            {
                UpdateContainer(db, container);
            }

            Section sectionInDb = db.Section.SingleOrDefault(x => x.Id == section.Id);

            // Check if deleted
            if (section.Deleted)
            {
                if (sectionInDb != null)
                {
                    db.Section.Remove(sectionInDb);
                }
                foreach (var container in section.Container)
                {
                    container.Deleted = true;
                }
            }
            else
            {
                // Check if new or updated
                if (sectionInDb == null)
                {
                    db.Section.Add(section);
                }
                else
                {
                    db.Entry(sectionInDb).CurrentValues.SetValues(section);
                }
            }
            
        }

        public void UpdateContainer(QuestionnaireDBContext db, Container container)
        {

            // Update children
            foreach (var answer in container.Answer)
            {
                UpdateAnswer(db, answer);
            }

            // Check foreing keys
            if (container.Sentence != null)
            {
                container.QuestionSentenceId = container.Sentence.Id;
                container.Sentence = null;
            }

            Container containerInDb = db.Container.SingleOrDefault(x => x.Id == container.Id);
            // Check if deleted
            if (container.Deleted)
            {
                if (containerInDb != null)
                {
                    db.Container.Remove(containerInDb);
                }
                foreach (var answer in container.Answer)
                {
                    answer.Deleted = true;
                }
            }
            else
            {
                // Check if new or updated
                if (containerInDb == null)
                {
                    db.Container.Add(container);
                }
                else
                {
                    db.Entry(containerInDb).CurrentValues.SetValues(container);
                }
            }
        }

        public void UpdateAnswer(QuestionnaireDBContext db, Answer answer)
        {
            Answer answerInDb = db.Answer.SingleOrDefault(x => x.Id == answer.Id);

            // Check children
            if (answer.Sentence != null)
            {
                answer.SentenceId = answer.Sentence.Id;
                answer.Sentence = null;
            }

            // Check if deleted
            if (answer.Deleted)
            {
                if (answerInDb != null)
                {
                    db.Answer.Remove(answerInDb);
                }
            }
            else
            {
                // Check if new or updated
                if (answerInDb == null)
                {
                    db.Answer.Add(answer);
                }
                else
                {
                    db.Entry(answerInDb).CurrentValues.SetValues(answer);
                }
            }
        }

        public List<Questionnaire> GetAllFull()
        {
            using (var db = new QuestionnaireDBContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var quer = db.Questionnaire.OrderBy(q => q.Date).
                    Include(q => q.Section.Select(s => s.Container.Select(c => c.Answer.Select(a => a.Sentence))))
                    .Include(q => q.Section.Select(s => s.Container.Select(c => c.Sentence)));
                var query = from q in db.Questionnaire
                            orderby q.Date
                            select q;
                return quer.ToList();
            }
        }

        public List<Questionnaire> GetAll()
        {
            using (var db = new QuestionnaireDBContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var quer = db.Questionnaire.OrderBy(q => q.Date);
                var query = from q in db.Questionnaire
                            orderby q.Date
                            select q;
                return quer.ToList();
            }
        }

        public bool Delete(int id)
        {
            using (var db = new QuestionnaireDBContext())
            {
                Questionnaire q = db.Questionnaire.SingleOrDefault(x => x.Id == id);
                if (q == null) return false;
                db.Questionnaire.Remove(q);
                db.SaveChanges();
                return true;
            }
        }

        public Questionnaire Get(int id)
        {
            using (var db = new QuestionnaireDBContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var quer = db.Questionnaire.OrderBy(q => q.Date).
                    Include(q => q.Section.Select(s => s.Container.Select(c => c.Answer.Select(a => a.Sentence))))
                    .Include(q => q.Section.Select(s => s.Container.Select(c => c.Sentence)))
                    .Where(x=>x.Id== id);
                return quer.SingleOrDefault();
            }
        }
    }
}
