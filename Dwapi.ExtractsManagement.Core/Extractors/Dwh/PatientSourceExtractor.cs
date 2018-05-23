using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Extractors.Dwh
{
    public class PatientSourceExtractor: IPatientSourceExtractor
    {
        private readonly IPatientSourceReader _reader;

        public PatientSourceExtractor(IPatientSourceReader reader)
        {
            _reader = reader;
        }

        public async Task<bool> Extract(DbExtract extract, DbProtocol dbProtocol)
        {
            // TODO: Notify started...

            var list = new List<TempPatientExtract>();

            int count = 0;
            using (var rdr = await _reader.ExecuteReader(dbProtocol,extract))
            {
                while (rdr.Read())
                {
                    count++;
                    // AutoMapper profiles
                    var extractRecord = Mapper.Map<IDataRecord, TempPatientExtract>(rdr);
                    
                    list.Add(extractRecord);

                    // TODO: batch and save

                    // TODO: Notify progress...
                }
            }

            // TODO: Notify Completed;

            return true;
        }
    }
}