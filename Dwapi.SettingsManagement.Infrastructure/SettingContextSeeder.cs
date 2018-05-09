using Dwapi.SettingsManagement.Core;
using Dwapi.SettingsManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.SettingsManagement.Infrastructure
{
    public class SettingContextSeeder
    {
        private readonly ISettingsUnitOfWork _unitOfWork;
        public SettingContextSeeder(ISettingsUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Seed()
        {

        }

        private void SetUpDockets()
        {
            
        }

    }
}
