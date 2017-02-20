using QuestionnaireDB.Repositories;
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

            var q = Test_AddNewQuestionnaire();
            Test_AddNewQuestionnaireWithSection();
            Test_DeleteQuestionnaire(q);

            //using (var db = new QuestionnaireDBContext())
            //{
            //    //Container container1 = new Container();
            //    //container1.Sentence = new Sentence() { Text = "New sentence at "+DateTime.Now};

            //    //Container container2 = new Container();
            //    //container2.QuestionSentenceId = 1058;

            //    //Section section1 = new Section();
            //    //section1.Description = "Section 1";
            //    //section1.Container = new List<Container>() { container1,container2 };

            //    //var questionnaire = new Questionnaire()
            //    //{
            //    //    Date = DateTime.Now,
            //    //    Description = "New questionnaire from  c# at "+DateTime.Now,
            //    //    Section = new List<Section>() { section1, }
            //    //};
            //    //db.Questionnaire.Add(questionnaire);

            //    //for (int i = 0; i < 10; i++)
            //    //{
            //    //    db.Sentence.Add(new Sentence() { Text = "TestSentence" + i });
            //    //}

            //    //db.SaveChanges();


            //    var q = db.Questionnaire.Single(x => x.Id == 1);

            //    q.Section.Add(new Section() { Description = "New section", Container = new List<Container>() { new Container() } });

            //    QuestionnaireDB.Repositories.QuestionnaireRepository repo = new Repositories.QuestionnaireRepository();
            //    repo.Add(q);

            //    //var query = from q in db.Questionnaire
            //    //    orderby q.Date
            //    //    select q;
            //    //foreach (var item in query)
            //    //{
            //    //    Console.WriteLine(item.Date.ToShortDateString() + " - " + item.Description);
            //    //}
            //}
            Console.ReadLine();
        }

        private static Questionnaire Test_AddNewQuestionnaire()
        {
            var repo =new  QuestionnaireRepository();
            var q= repo.UpdateQuestionnaire(new Questionnaire() { Description = "Empty Questionnaire", Date = DateTime.Now });
            if (q.Id != 0)
            {
                Console.WriteLine("Test_AddNewQuestionnaire Passed");
            }
            else
            {
                Console.WriteLine("Test_AddNewQuestionnaire NOT Passed");
            }

            return q;
        }

        private static void Test_DeleteQuestionnaire(Questionnaire q)
        {
            var repo = new QuestionnaireRepository();
            repo.Delete(q.Id);
            using (var db = new QuestionnaireDBContext())
            {
                if (!db.Questionnaire.Any(x=>x.Id==q.Id))
                {
                    Console.WriteLine("Test_DeleteQuestionnaire Passed");
                }
                else
                {
                    Console.WriteLine("Test_DeleteQuestionnaire NOT Passed");
                }
            }
        }

        private static Questionnaire Test_AddNewQuestionnaireWithSection()
        {
            var repo = new QuestionnaireRepository();
            var questionnaire = new Questionnaire() { Description = "Empty Questionnaire", Date = DateTime.Now };
            questionnaire.Section = new List<Section>();
            questionnaire.Section.Add(new Section() { Description = "New section" });
            Questionnaire q = repo.UpdateQuestionnaire(questionnaire);
            if (q.Id != 0 && q.Section.Count > 0 && q.Section.First().Id != 0)
            {
                Console.WriteLine("Test_AddNewQuestionnaireWithSection Passed");
            }
            else
            {
                Console.WriteLine("Test_AddNewQuestionnaireWithSection NOT Passed");
            }

            return q;
        }

        private static Questionnaire Test_AddSectionInExistingQuestionnaire(Questionnaire questionnaire)
        {
            using (var db = new QuestionnaireDBContext())
            {
                Questionnaire qFromDb = db.Questionnaire.First();
                Questionnaire qFromApi = new Questionnaire() { Id = qFromDb.Id, Description = qFromDb.Description, Date = qFromDb.Date };
                

            }
            
            var repo = new QuestionnaireRepository();
            var description = "NesSection" + DateTime.Now;
            Section section = new Section() { Description = description };

            
            var q = repo.UpdateQuestionnaire(questionnaire);
            if (q.Id != 0 && q.Section.Count > 0 && q.Section.Single(s => s.Description == description) != null)
            {
                Console.WriteLine("Test_AddSectionInExistingQuestionnaire Passed");
            }
            else
            {
                Console.WriteLine("Test_AddSectionInExistingQuestionnaire NOT Passed");
            }

            return q;
        }


    }
}
