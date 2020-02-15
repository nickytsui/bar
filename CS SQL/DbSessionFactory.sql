using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using IDAL;
using System.Threading;

namespace DalFactory
{
    public class DbSessionFactory
    {
        public static IDAL.IDbSession GetCurrentDbSession()
        {
            /// return   new DbSession();

            IDAL.IDbSession dbSession = (IDbSession)CallContext.GetData("DbSession");

            int threadId = Thread.CurrentThread.ManagedThreadId;

            if (dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData("DbSession", dbSession);
            }


            return dbSession;
        }
    }
}
