using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB
{
    public interface  IMyTable
    {
        int Id { get; set; }
        bool Deleted { get; set; }
    }

    partial class Answer : IMyTable
    {
        public bool Deleted { get; set; }
    }
    partial class Container : IMyTable
    {
        public bool Deleted { get; set; }
    }
    partial class Questionnaire : IMyTable
    {
        public bool Deleted { get; set; }
    }
    partial class Section : IMyTable
    {
        public bool Deleted { get; set; }
    }
    partial class Sentence : IMyTable
    {
        public bool Deleted { get; set; }
    }
}
