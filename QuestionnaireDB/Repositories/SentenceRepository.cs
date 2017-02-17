using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB.Repositories
{
    public class SentenceRepository
    {
        public List<Sentence> GetAll()
        {
            using (var db = new QuestionnaireDBContext())
            {
                var query = from s in db.Sentence
                            orderby s.Text
                            select s;
                return query.ToList();
            }
        }

        public Sentence Get(int id)
        {
            using (var db = new QuestionnaireDBContext())
            {
                var query = from s in db.Sentence
                            where s.Id == id
                            select s;
                return query.SingleOrDefault();
            }
        }

        public Sentence Save(Sentence sentence)
        {
            Sentence toReturn = null;
            using (var db = new QuestionnaireDBContext())
            {
                var sentenceInDb = db.Sentence.FirstOrDefault(x=>x.Text.ToLower().Equals(sentence.Text.ToLower()));
                if (sentenceInDb == null)
                {
                    toReturn = db.Sentence.Add(sentence);
                    db.SaveChanges();
                }
                else
                    toReturn = sentenceInDb;
            }
            return toReturn;
        }

        public List<Sentence> Save(List<Sentence> sentences)
        {
            List<Sentence> toReturn = null;
            using (var db = new QuestionnaireDBContext())
            {
                toReturn = db.Sentence.AddRange(sentences).ToList();
                db.SaveChanges();
            }
            return toReturn;
        }

        public List<Sentence> GetByText(string text)
        {
            List<Sentence> toReturn = null;
            using (var db = new QuestionnaireDBContext())
            {
                toReturn = db.Sentence.Where(x => x.Text.ToLower().Contains(text)).ToList();
            }
            return toReturn;
        }

        public Sentence Delete(Sentence sentence)
        {
            Sentence toReturn = null;
            using (var db = new QuestionnaireDBContext())
            {
                Sentence toDelete = db.Sentence.FirstOrDefault(x => x.Id == sentence.Id);
                toDelete.Delete(db);
                db.SaveChanges();
            }
            return toReturn;
        }
    }
}
