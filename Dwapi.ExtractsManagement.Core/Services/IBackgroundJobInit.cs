using Hangfire;
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
    }

    public class BackgroundJobInit : IBackgroundJobInit
    {
        public void EnqueueJob(Expression<Action> methodCall)
        {
            BackgroundJob.Enqueue(methodCall);
        }
    }
}
