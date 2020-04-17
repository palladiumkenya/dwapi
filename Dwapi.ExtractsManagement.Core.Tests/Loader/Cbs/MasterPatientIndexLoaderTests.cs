// using System;
// using System.Linq;
// using AutoMapper;
// using AutoMapper.Data;
// using Dwapi.ExtractsManagement.Core.Cleaner.Cbs;
// using Dwapi.ExtractsManagement.Core.ComandHandlers.Cbs;
// using Dwapi.ExtractsManagement.Core.Extractors.Cbs;
// using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
// using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
// using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Cbs;
// using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
// using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
// using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs;
// using Dwapi.ExtractsManagement.Core.Loader.Cbs;
// using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
// using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
// using Dwapi.ExtractsManagement.Core.Profiles.Cbs;
// using Dwapi.ExtractsManagement.Infrastructure;
// using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
// using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
// using Dwapi.ExtractsManagement.Infrastructure.Validators.Cbs;
// using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
// using Dwapi.SettingsManagement.Core.Model;
// using Dwapi.SettingsManagement.Infrastructure.Repository;
// using Dwapi.SharedKernel.Model;
// using Dwapi.SharedKernel.Utility;
// using FizzWare.NBuilder;
// using MediatR;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using NUnit.Framework;
//
// namespace Dwapi.ExtractsManagement.Core.Tests.Loader.Cbs
// {
//     [TestFixture]
//     public class MasterPatientIndexLoaderTests
//     {
//         private ExtractsContext _extractsContext, _extractsContextMySql;
//         private DbProtocol _iQtoolsDb, _kenyaEmrDb;
//         [OneTimeSetUp]
//         public void Init()
//         {
//             var extractId = TestInitializer.Iqtools.Extracts.First(x => x.Name.IsSameAs(nameof(MasterPatientIndex))).Id;
//             var cleaner = TestInitializer.ServiceProvider.GetService<ICleanCbsExtracts>();
//             cleaner.Clean(extractId);
//
//             var extractIdMySql = TestInitializer.KenyaEmr.Extracts.First(x => x.Name.IsSameAs(nameof(MasterPatientIndex))).Id;
//             var cleanerMySql = TestInitializer.ServiceProviderMysql.GetService<ICleanCbsExtracts>();
//             cleanerMySql.Clean(extractIdMySql);
//
//             _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
//             _extractsContextMySql = TestInitializer.ServiceProviderMysql.GetService<ExtractsContext>();
//
//             var tempMpis = Builder<TempMasterPatientIndex>.CreateListOfSize(2).Build().ToList();
//
//             _extractsContext.TempMasterPatientIndices.AddRange(tempMpis);
//             _extractsContext.SaveChanges();
//
//             _extractsContextMySql.TempMasterPatientIndices.AddRange(tempMpis);
//             _extractsContextMySql.SaveChanges();
//
//             _iQtoolsDb = TestInitializer.Iqtools.DatabaseProtocols.First(x => x.DatabaseName.ToLower().Contains("iqtools".ToLower()));
//             _iQtoolsDb.Host = ".\\Koske14";
//             _iQtoolsDb.Username = "sa";
//             _iQtoolsDb.Password = "maun";
//
//             _kenyaEmrDb = TestInitializer.KenyaEmr.DatabaseProtocols.First();
//             _kenyaEmrDb.Host = "127.0.0.1";
//             _kenyaEmrDb.Username = "root";
//             _kenyaEmrDb.Password = "test";
//             _kenyaEmrDb.DatabaseName = "openmrs";
//         }
//
//
//         [Test]
//         public void should_Load_From_Temp_MsSQL()
//         {
//             Assert.False(_extractsContext.MasterPatientIndices.Any());
//             var extract = TestInitializer.Iqtools.Extracts.First(x => x.DocketId.IsSameAs("CBS"));
//
//             var loader = TestInitializer.ServiceProvider.GetService<IMasterPatientIndexLoader>();
//
//             var loadCount=  loader.Load(extract.Id, 0).Result;
//             Assert.True(_extractsContext.MasterPatientIndices.Any());
//             Console.WriteLine($"extracted {_extractsContext.MasterPatientIndices.Count()}");
//         }
//
//         [Test]
//         public void should_Load_From_Temp_MySQL()
//         {
//             Assert.False(_extractsContextMySql.MasterPatientIndices.Any());
//             var extract = TestInitializer.KenyaEmr.Extracts.First(x => x.DocketId.IsSameAs("CBS"));
//
//             var loader = TestInitializer.ServiceProviderMysql.GetService<IMasterPatientIndexLoader>();
//
//             var loadCount = loader.Load(extract.Id, 0).Result;
//             Assert.True(_extractsContextMySql.MasterPatientIndices.Any());
//             Console.WriteLine($"extracted {_extractsContextMySql.MasterPatientIndices.Count()}");
//         }
//     }
// }
