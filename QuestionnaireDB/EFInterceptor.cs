﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireDB
{
    public class EFInterceptor : IDbCommandInterceptor
    {

        public void NonQueryExecuted(System.Data.Common.DbCommand command,
            DbCommandInterceptionContext<int> interceptionContext)
        {
            LogInfo("NonQueryExecuted",
                String.Format(" IsAsync: {0}, Command Text: {1}", interceptionContext.IsAsync, command.CommandText),command.Parameters);
        }

        public void NonQueryExecuting(System.Data.Common.DbCommand command,
            DbCommandInterceptionContext<int> interceptionContext)
        {
            LogInfo("NonQueryExecuting",
                String.Format(" IsAsync: {0}, Command Text: {1}", interceptionContext.IsAsync, command.CommandText),command.Parameters);
        }

        public void ReaderExecuting(System.Data.Common.DbCommand command,
            DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            LogInfo("ReaderExecuting",
                String.Format(" IsAsync: {0}, Command Text: {1}", interceptionContext.IsAsync, command.CommandText),command.Parameters);
        }

        public void ScalarExecuted(System.Data.Common.DbCommand command,
            DbCommandInterceptionContext<object> interceptionContext)
        {
            LogInfo("ScalarExecuted",
                String.Format(" IsAsync: {0}, Command Text: {1}", interceptionContext.IsAsync, command.CommandText),command.Parameters);
        }

        public void ScalarExecuting(System.Data.Common.DbCommand command,
            DbCommandInterceptionContext<object> interceptionContext)
        {
            LogInfo("ScalarExecuting",
                String.Format(" IsAsync: {0}, Command Text: {1}", interceptionContext.IsAsync, command.CommandText),command.Parameters);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogInfo("ReaderExecuted", String.Format(" IsAsync: {0}, Command Text: {1}", interceptionContext.IsAsync, command.CommandText),command.Parameters);
        }

        private void LogInfo(string command, string commandText, DbParameterCollection parameters)
        {
            System.Diagnostics.Debug.WriteLine("Intercepted on: {0} :\n {1} ", command, commandText);
            int i = 0;
            foreach (DbParameter param in parameters)
            {
                System.Diagnostics.Debug.Write("@" + i++ + ": " + param.Value+" | ");
            }
        }


    }
}
