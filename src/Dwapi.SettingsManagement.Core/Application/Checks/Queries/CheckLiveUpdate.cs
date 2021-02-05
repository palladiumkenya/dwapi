using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Dwapi.SettingsManagement.Core.DTOs;
using MediatR;
using Serilog;

namespace Dwapi.SettingsManagement.Core.Application.Checks.Queries
{
    public class CheckLiveUpdate:IRequest<Result<AppVerDto>>
    {
        public string LocalVersion { get;  }

        public CheckLiveUpdate(string localVersion)
        {
            LocalVersion = localVersion;
        }
    }

    public class CheckLiveUpdateHandler : IRequestHandler<CheckLiveUpdate,Result<AppVerDto>>
    {
        public async Task<Result<AppVerDto>> Handle(CheckLiveUpdate request, CancellationToken cancellationToken)
        {
            var client = new HttpClient();

            try
            {
                var response =
                    await client.GetAsync("https://data.kenyahmis.org:444/dwapi/client/updates/livesync.txt");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    return Result.Success<AppVerDto>(new AppVerDto(request.LocalVersion, content));
                }
            }
            catch (Exception e)
            {
                Log.Error(e, $"Send Manifest Error");
                return Result.Failure<AppVerDto>(e.Message);
            }

            return Result.Failure<AppVerDto>("Unkown Error");
        }
    }
}
