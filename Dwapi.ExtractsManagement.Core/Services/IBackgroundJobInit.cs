using Hangfire;
using Hangfire.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public interface IBackgroundJobInit
    {
        string EnqueueJob(Expression<Action> methodCall);
        void EnqueueMany(params Expression<Action>[] methodCalls);
        void ChainJobs(IList<Expression<Action>> methodCalls);
        void ChainJobsAfterFirst(IList<Expression<Action>> methodCalls);
    }

    public class BackgroundJobInit : IBackgroundJobInit
    {
        public string EnqueueJob(Expression<Action> methodCall)
        {
            JobHelper.SetSerializerSettings(
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            return BackgroundJob.Enqueue(methodCall);
        }

        public void EnqueueMany(params Expression<Action>[] methodCalls)
        {
            foreach (var methodCall in methodCalls)
                EnqueueJob(methodCall);
        }
        

        public void ChainJobs(IList<Expression<Action>> methodCalls)
        {
            string jobId = null;
            foreach(var methodCall in methodCalls)
            {
                if (string.IsNullOrWhiteSpace(jobId))
                {
                    jobId = BackgroundJob.Enqueue(methodCall);
                    continue;
                }

                jobId = BackgroundJob.ContinueWith(jobId, methodCall);
            }
        }

        public void ChainJobsAfterFirst(IList<Expression<Action>> methodCalls)
        {
            if (!methodCalls.Any()) return;
            var jobId = BackgroundJob.Enqueue(methodCalls.First());

            if (string.IsNullOrWhiteSpace(jobId)) return;
            methodCalls.Remove(methodCalls.First());

            foreach (var methodCall in methodCalls)
                BackgroundJob.ContinueWith(jobId, methodCall, 
                    JobContinuationOptions.OnlyOnSucceededState);
        }
    }
}
