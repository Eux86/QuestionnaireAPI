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
        public Questionnaire Save(Questionnaire questionnaire)
        {
            Questionnaire toReturn = null;
            using (var db = new QuestionnaireDBContext())
            {
                Questionnaire questInDb = db.Questionnaire.SingleOrDefault(x => x.Id == questionnaire.Id);
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
                    if (questInDb == null)
                    {
                        questInDb = db.Questionnaire.Add(questionnaire);
                    }
                    else
                    {
                        db.Entry(questInDb).CurrentValues.SetValues(questionnaire);
                    }
                }
                foreach (var section in questionnaire.Section)
                {
                    Section sectionInDb = db.Section.SingleOrDefault(x => x.Id == section.Id);
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
                        if (sectionInDb == null)
                        {
                            sectionInDb = db.Section.Add(section);
                        }
                        else
                        {
                            db.Entry(sectionInDb).CurrentValues.SetValues(section);
                        }
                    }
                    foreach (var container in section.Container)
                    {
                        Container containerInDb = db.Container.SingleOrDefault(x => x.Id == container.Id);

                        foreach (var answer in container.Answer)
                        {
                            Answer answerInDb = db.Answer.SingleOrDefault(x => x.Id == answer.Id);

                            if (answer.Deleted)
                            {
                                if (answerInDb != null)
                                {
                                    db.Answer.Remove(answerInDb);
                                }
                            }
                            else
                            {
                                if (answerInDb == null)
                                {
                                    // Avoid creating a new sentence. It shouldn't be created here.
                                    answer.SentenceId = answer.Sentence.Id;
                                    answer.Sentence = null;
                                    db.Answer.Add(answer);
                                }
                                else
                                {
                                    db.Entry(answerInDb).CurrentValues.SetValues(answer);
                                }
                            }
                        }

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
                            if (containerInDb == null)
                            {
                                // Avoid creating a new sentence. It shouldn't be created here.
                                container.QuestionSentenceId = container.Sentence.Id;
                                container.Sentence = null;
                                db.Container.Add(container);
                            }
                            else
                            {
                                db.Entry(containerInDb).CurrentValues.SetValues(container);
                            }
                        }
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
