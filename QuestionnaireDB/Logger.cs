using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB
{
    static class Logger
    {
        public static void Log(string message, EventLogEntryType type, Exception exception = null)
        {
            EventLog.WriteEntry("QuestionnaireDB", message+Environment.NewLine+(exception!=null?exception.StackTrace:""), type);
        }
    }
}
