using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB.Repositories
{
    public class FileRepository
    {
        public File Save(File file)
        {
            using (QuestionnaireDBContext db = new QuestionnaireDBContext())
            {
                db.File.Add(file);
                db.SaveChanges();
            }
            return file;
        }

        public File Get(int id)
        {
            using (QuestionnaireDBContext db = new QuestionnaireDBContext())
            {
                return db.File.ToList().SingleOrDefault(x => x.id == id);
            }
        }
    }
}
