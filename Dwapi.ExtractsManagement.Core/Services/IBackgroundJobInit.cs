using Hangfire;
using Hangfire.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public interface IBackgroundJobInit
    {
        void EnqueueJob(Expression<Action> methodCall);
        void EnqueueMany(params Expression<Action>[] methodCalls);
    }

    public class BackgroundJobInit : IBackgroundJobInit
    {
        public void EnqueueJob(Expression<Action> methodCall)
        {
            JobHelper.SetSerializerSettings(
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            BackgroundJob.Enqueue(methodCall);
        }

        public void EnqueueMany(params Expression<Action>[] methodCalls)
        {
            foreach (var methodCall in methodCalls)
                EnqueueJob(methodCall);
        }
    }
}
