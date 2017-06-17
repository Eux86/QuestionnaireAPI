using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB.Repositories
{
    public class TranslationRepository
    {
        public bool Delete(int[] ids)
        {
            using (var db = new QuestionnaireDBContext())
            {
                var translations = db.Translation.Where(x => ids.Contains((x.Id)));
                if (!translations.Any()) return false;
                db.Translation.RemoveRange(translations);
                db.SaveChanges();
                return true;
            }
        }

        public bool Delete(int id)
        {
            return Delete(new int[] { id });
        }

        public bool Update(IEnumerable<Translation> translations)
        {
            using (var db = new QuestionnaireDBContext())
            {
                var now = DateTime.Now;
                foreach (var t in translations)
                {
                    Translation toUpdate = db.Translation.SingleOrDefault(x => x.Id == t.Id);
                    if (toUpdate == null)
                    {
                        Console.Error.WriteLine("Can't find Translation with ID: " + t.Id);
                        return false;
                    }
                    toUpdate.Key = t.Key;
                    toUpdate.Value = t.Value;
                    toUpdate.LatestUpdate = now;
                }

                db.SaveChanges();
                return true;
            }
        }

        public bool Update(Translation translation)
        {
            return Update(new List<Translation>() {translation});
        }

        public bool Add(IEnumerable<Translation> translations)
        {
            using (var db = new QuestionnaireDBContext())
            {
                var now = DateTime.Now;
                foreach (var t in translations)
                {
                    t.LatestUpdate = now;
                }
                db.Translation.AddRange(translations);
                db.SaveChanges();
                return true;
            }
        }

        public bool Add(Translation translation)
        {
            return Add(new List<Translation>() { translation });
        }

        public DateTime GetLatestVersion()
        {
            using (var db = new QuestionnaireDBContext())
            {
                return db.Translation.Select(x => x.LatestUpdate).Max();
            }
        }

        public IEnumerable<Translation> GetAll()
        {
            using (var db = new QuestionnaireDBContext())
            {
                return db.Translation.ToList();
            }
        } 


    }
}
