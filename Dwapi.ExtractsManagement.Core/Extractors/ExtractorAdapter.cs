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

    public class ExtractorValidatorAdapter
    {
        private IDictionary<ExtractType, IExtractorValidator> _dict
            = new Dictionary<ExtractType, IExtractorValidator>();

        public void RegisterExtractorValidator(ExtractType extractType, IExtractorValidator extractor)
        {
            if (extractor != null)
                _dict.Add(extractType, extractor);
        }

        public IExtractorValidator GetExtractorValidator(ExtractType extractType)
        {
            if (_dict.TryGetValue(extractType, out IExtractorValidator extractor))
                return extractor;
            else
                throw new InvalidOperationException($"ExtractorValidator for type {extractType.ToString()} not registered");
        }
    }
}
