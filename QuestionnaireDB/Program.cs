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
                Container container1 = new Container();
                container1.Sentence = new Sentence() { Text = "New sentence at "+DateTime.Now};

                Container container2 = new Container();
                container2.QuestionSentenceId = 1058;

                Section section1 = new Section();
                section1.Description = "Section 1";
                section1.Container = new List<Container>() { container1,container2 };

                var questionnaire = new Questionnaire()
                {
                    Date = DateTime.Now,
                    Description = "New questionnaire from  c# at "+DateTime.Now,
                    Section = new List<Section>() { section1, }
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
