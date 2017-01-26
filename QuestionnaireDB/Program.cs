using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new QuestionnaireDBContext())
            {
                var questionnaire = new Questionnaire()
                {
                    Date = DateTime.Now,
                    Description = "First questionnaire from c#"
                };
                db.Questionnaire.Add(questionnaire);

                for (int i = 0; i < 10; i++)
                {
                    db.Sentence.Add(new Sentence() { Text = "TestSentence" + i });
                }

                db.SaveChanges();

                var query = from q in db.Questionnaire
                    orderby q.Date
                    select q;
                foreach (var item in query)
                {
                    Console.WriteLine(item.Date.ToShortDateString() + " - " + item.Description);
                }
            }
            Console.ReadLine();
        }
    }
}
