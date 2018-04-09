using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public interface IExtractorAdapter
    {
        void RegisterExtractor(string emr, IDataExtractor dataExtractor);
        IDataExtractor GetExtractor(string emr);
    }
}
