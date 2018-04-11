using Dwapi.ExtractsManagement.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    public class ExtractorAdapter
    {
        private IDictionary<ExtractType, IExtractor> _dict 
            = new Dictionary<ExtractType, IExtractor>();

        public void RegisterExtractor(ExtractType extractType, IExtractor extractor)
        {
            if (extractor != null)
                _dict.Add(extractType, extractor);
        }

        public IExtractor GetExtractor(ExtractType extractType)
        {
            if (_dict.TryGetValue(extractType, out IExtractor extractor))
                return extractor;
            else
                throw new InvalidOperationException($"Extractor for type {extractType.ToString()} not registered");
        }
    }
}
