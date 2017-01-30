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

        QuestionnaireDBContext _db = new QuestionnaireDBContext();

        public Questionnaire Save(Questionnaire questionnaire)
        {
            Questionnaire toReturn = null;

            Questionnaire existingQuest = _db.Questionnaire.Single(x => x.Id == questionnaire.Id);
            if (existingQuest != null)
            {
                existingQuest.Description = questionnaire.Description;
                List<Section> toRemove = null;
                List<Section> toUpdate = null;
                List<Section> toAdd = null;
                if (existingQuest.Section != null && existingQuest.Section.Count > 0)
                {
                    toRemove = existingQuest.Section.Where(x => !questionnaire.Section.Any(s=>s.Id == x.Id)).ToList();
                    toUpdate = existingQuest.Section.Where(x => questionnaire.Section.Any(s => s.Id == x.Id)).ToList();
                }
                if (questionnaire.Section != null && questionnaire.Section.Count > 0)
                {
                    toAdd = questionnaire.Section.Where(x => x.Id == 0).ToList();
                }

                _db.Section.RemoveRange(toRemove);
                _db.Section.AddRange(toAdd);
                foreach (var s in toUpdate)
                {
                    // Save(s);
                }

                toReturn = existingQuest;
            }
            else
            {
                toReturn = _db.Questionnaire.Add(questionnaire);
            }

            _db.SaveChanges();
            return toReturn;
        }

        //public Questionnaire Add(Questionnaire questionnaire)
        //{
        //    Questionnaire savedQuestionnaire = null;
                
        //    var oldQuestionnaire = _db.Questionnaire.SingleOrDefault(q => q.Id == questionnaire.Id);
        //    if (oldQuestionnaire != null)
        //    {
        //        List<Section> toRemove = new List<Section>();
        //        foreach (var s in oldQuestionnaire.Section)
        //        {
        //            if (!questionnaire.Section.Contains(s))
        //                _db.Section.RemoveRange(toRemove);
        //        }
        //        _db.Section.RemoveRange(toRemove);
                
        //        oldQuestionnaire.Description = questionnaire.Description;
        //        oldQuestionnaire.Date = questionnaire.Date;
        //        foreach (var s in questionnaire.Section)
        //        {
        //            CreateOrSave(s, oldQuestionnaire.Id);
        //        }
        //        savedQuestionnaire = oldQuestionnaire;
        //    }
        //    else
        //    {
        //        savedQuestionnaire = _db.Questionnaire.Add(questionnaire);
        //    }

        //    _db.SaveChanges();
        //    return savedQuestionnaire;
        //}

        //private void CreateOrSave(Section section, int parentId)
        //{
        //    var oldSection = _db.Section.SingleOrDefault(s => s.Id == section.Id);
        //    if (oldSection != null)
        //    {
        //        List<Container> toRemove = new List<Container>();
        //        foreach (var c in oldSection.Container)
        //        {
        //            if (!section.Container.Contains(c))
        //                toRemove.Add(c);
        //        }
        //        _db.Container.RemoveRange(toRemove);

        //        oldSection.Description = section.Description;
        //        oldSection.QuestionnaireId = section.QuestionnaireId;
        //        foreach (var c in section.Container)
        //        {
        //            CreateOrSave(c, section.Id);
        //        }
        //    }
        //    else
        //    {
        //        section.QuestionnaireId = parentId;
        //        _db.Section.Add(section);
        //    }

        //}

        //private void CreateOrSave(Container container, int parentId)
        //{
        //    var oldcontainer = _db.Container.SingleOrDefault(s => s.Id == container.Id);
        //    if (oldcontainer != null)
        //    {
        //        List<Answer> toRemove = new List<Answer>();
        //        foreach (var c in oldcontainer.Answer)
        //        {
        //            if (!container.Answer.Contains(c))
        //                toRemove.Add(c);
        //        }
        //        _db.Answer.RemoveRange(toRemove);

        //        oldcontainer.IsRightAnswered = container.IsRightAnswered;
        //        oldcontainer.RightAnswerId = container.RightAnswerId;
        //        foreach (var c in container.Answer)
        //        {
        //            CreateOrSave(c, oldcontainer.Id);
        //        }
        //    }
        //    else
        //    {
        //        container.SectionId = parentId;
        //        _db.Container.Add(container);
        //    }
        //}

        //private void CreateOrSave(Answer answer, int parentId)
        //{
        //    var oldAnswer = _db.Answer.SingleOrDefault(s => s.Id == answer.Id);
        //    if (oldAnswer != null)
        //    {
        //        oldAnswer.Selected = answer.Selected;
        //    }
        //    else
        //    {
        //        answer.ContainerID = parentId;
        //        _db.Answer.Add(answer);
        //    }
        //}

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
