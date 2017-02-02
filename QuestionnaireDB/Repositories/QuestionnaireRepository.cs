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
                toReturn = questInDb;
                foreach (var section in questionnaire.Section)
                {
                    Section sectionInDb = db.Section.SingleOrDefault(x => x.Id == section.Id);
                    if (section.Deleted)
                    {
                        if (sectionInDb != null)
                        {
                            db.Section.Remove(sectionInDb);
                        }
                    }
                    else
                    {
                        if (section.Id == null)
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
                        Sentence sentenceInDb = db.Sentence.SingleOrDefault(x => x.Id == container.Sentence.Id);

                        if (container.Deleted)
                        {
                            if (containerInDb != null)
                            {
                                db.Container.Remove(containerInDb);
                            }
                        }
                        else
                        {
                            if (containerInDb == null)
                            {
                                if (sentenceInDb != null)
                                {
                                    container.Sentence = sentenceInDb;
                                }
                                containerInDb = db.Container.Add(container);
                            }
                            else
                            {
                                db.Entry(containerInDb).CurrentValues.SetValues(container);
                            }
                        }
                        if (container.Sentence != null)
                        {
                            if (container.Deleted)
                            {
                                if (sentenceInDb != null)
                                {
                                    db.Sentence.Remove(sentenceInDb);
                                }
                            }
                            else
                            {
                                if (sentenceInDb == null)
                                {
                                    sentenceInDb = db.Sentence.Add(container.Sentence);
                                    container.QuestionSentenceId = sentenceInDb.Id;
                                }
                                else
                                {
                                    db.Entry(sentenceInDb).CurrentValues.SetValues(container.Sentence);
                                }
                            }
                        }

                        foreach (var answer in container.Answer)
                        {
                            Answer answerInDb = db.Answer.SingleOrDefault(x => x.Id == answer.Id);
                            sentenceInDb = db.Sentence.SingleOrDefault(x => x.Id == answer.Sentence.Id);

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
                                    if (sentenceInDb != null)
                                    {
                                        answer.Sentence = sentenceInDb;
                                    }
                                    answerInDb = db.Answer.Add(answer);
                                }
                                else
                                {
                                    db.Entry(answerInDb).CurrentValues.SetValues(answer);
                                }
                            }
                            if (answer.Sentence != null)
                            {
                                
                                if (answer.Deleted)
                                {
                                    if (sentenceInDb != null)
                                    {
                                        db.Sentence.Remove(sentenceInDb);
                                    }
                                }
                                else
                                {
                                    if (sentenceInDb == null)
                                    {
                                        sentenceInDb = db.Sentence.Add(answer.Sentence);
                                        answer.SentenceId = sentenceInDb.Id;
                                    }
                                    else
                                    {
                                        db.Entry(sentenceInDb).CurrentValues.SetValues(answer.Sentence);
                                    }
                                }
                            }
                        }
                    }
                }
                db.SaveChanges();

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
    }
}
