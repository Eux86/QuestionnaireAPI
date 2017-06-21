using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB.Repositories
{
    public class LanguageRepository
    {
        public List<Language> GetAll()
        {
            using (QuestionnaireDBContext db = new QuestionnaireDBContext())
            {
                return db.Language.ToList();
            }
        }

        public Language Add(Language l)
        {
            using (QuestionnaireDBContext db = new QuestionnaireDBContext())
            {
                try
                {
                    var newLanguage = db.Language.Add(l);
                    db.SaveChanges();
                    return newLanguage;
                }
                catch (Exception e)
                {
                    Logger.Log("LanguageRepository - Add", EventLogEntryType.Error, e);
                    return null;
                }
            }
        }

        public Language Delete(Language l)
        {
            using (QuestionnaireDBContext db = new QuestionnaireDBContext())
            {
                try
                {
                    Language languageToRemove = db.Language.ToList().Single(x=>x.Id==l.Id);
                    var translationsToRemove = db.Translation.ToList().Where(x => x.LanguageId == languageToRemove.Id);
                    db.Translation.RemoveRange(translationsToRemove);
                    var deleted = db.Language.Remove(languageToRemove);
                    db.SaveChanges();
                    return deleted;
                }
                catch (Exception e)
                {
                    Logger.Log("LanguageRepository - Remove", EventLogEntryType.Error, e);
                    return null;
                }
            }
        }


        public Language Activate(Language language)
        {
            using (QuestionnaireDBContext db = new QuestionnaireDBContext())
            {
                try
                {
                    var toDeactivate = db.Language.Where(x => x.Active).ToList();
                    toDeactivate.ForEach(x => x.Active = false);
                    var toActivate = db.Language.Single(x=>x.Id== language.Id);
                    toActivate.Active = true;

                    db.SaveChanges();
                    return toActivate;
                }
                catch (Exception e)
                {
                    Logger.Log("LanguageRepository - Activate", EventLogEntryType.Error, e);
                    return null;
                }
            }
        }
    }
}
