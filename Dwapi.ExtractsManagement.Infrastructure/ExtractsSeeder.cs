using Dwapi.ExtractsManagement.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Infrastructure
{
    public class ExtractsSeeder
    {
        private readonly IExtractUnitOfWork _unitOfWork;

        public ExtractsSeeder(IExtractUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public void Seed()
        {

        }

        private void SetUpDockets()
        {
        }
    }
}
