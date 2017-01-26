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
        public bool Add(Questionnaire questionnaire)
        {
            using (var db = new QuestionnaireDBContext())
            {
                //db.UpdateEntitiesState(questionnaire);

                // ========================================= KEEP EDITING HERE TO SAVE EACH PART OF THE QUESTIONNAIRE 

                foreach (var section in questionnaire.Section)
                {
                    var oldSection = db.Section.SingleOrDefault(s => s.Id == section.Id);
                    if (oldSection != null)
                    {
                        oldSection.Description = section.Description;
                        oldSection.QuestionnaireId = section.QuestionnaireId;
                    }
                    else
                    {
                        db.Section.Add(section);
                    }
                }

                var oldQuestionnaire = db.Questionnaire.SingleOrDefault(q => q.Id == questionnaire.Id);
                if (oldQuestionnaire != null)
                {
                    oldQuestionnaire.Description = questionnaire.Description;
                    oldQuestionnaire.Date = questionnaire.Date;
                }
                else
                {
                    db.Questionnaire.Add(questionnaire);
                }
                

                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Questionnaire> GetAllFull()
        {
            using (var db = new QuestionnaireDBContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var quer = db.Questionnaire.OrderBy(q => q.Date).
                    Include(q => q.Section.Select(s=>s.Container.Select(c=>c.Answer)))
                    .Include(q=> q.Section.Select(s=>s.Container.Select(c=>c.Sentence)));
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
