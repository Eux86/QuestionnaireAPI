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
                //var original = db.Questionnaire.Find(questionnaire.Id);
                db.UpdateEntitiesState(questionnaire);
                db.SaveChanges();
                toReturn = questionnaire;
            }
            return toReturn;
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
