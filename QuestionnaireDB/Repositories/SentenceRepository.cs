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
    }
}
