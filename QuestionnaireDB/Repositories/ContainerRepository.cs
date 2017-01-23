using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB.Repositories
{
    public class ContainerRepository
    {
        public Container Get(int id)
        {
            using (var db = new QuestionnaireDBContext())
            {
                return db.Container.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}
