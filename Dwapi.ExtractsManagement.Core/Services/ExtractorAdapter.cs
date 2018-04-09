using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public class ExtractorAdapter : IExtractorAdapter
    {
        private IDictionary<string, IDataExtractor> _dict 
            => new Dictionary<string, IDataExtractor>();

        public void RegisterExtractor(string emr, IDataExtractor dataExtractor)
        {
            if(dataExtractor==null) 
                throw new InvalidOperationException();
            _dict.Add(emr, dataExtractor);
        }

        public IDataExtractor GetExtractor(string emr)
        {
            if (_dict.TryGetValue(emr, out IDataExtractor dataExtractor))
                return dataExtractor;
            throw new InvalidOperationException($"Extractor for {emr} not registered");
        }
    }
}
