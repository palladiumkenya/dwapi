FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5757 5753

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/Dwapi/Dwapi.csproj src/Dwapi/
COPY src/Dwapi.SharedKernel/Dwapi.SharedKernel.csproj src/Dwapi.SharedKernel/
COPY src/Dwapi.SettingsManagement.Core/Dwapi.SettingsManagement.Core.csproj src/Dwapi.SettingsManagement.Core/
COPY src/Dwapi.SharedKernel.Infrastructure/Dwapi.SharedKernel.Infrastructure.csproj src/Dwapi.SharedKernel.Infrastructure/
COPY src/Dwapi.UploadManagement.Infrastructure/Dwapi.UploadManagement.Infrastructure.csproj src/Dwapi.UploadManagement.Infrastructure/
COPY src/Dwapi.UploadManagement.Core/Dwapi.UploadManagement.Core.csproj src/Dwapi.UploadManagement.Core/
COPY src/Dwapi.ExtractsManagement.Core/Dwapi.ExtractsManagement.Core.csproj src/Dwapi.ExtractsManagement.Core/
COPY src/Dwapi.SettingsManagement.Infrastructure/Dwapi.SettingsManagement.Infrastructure.csproj src/Dwapi.SettingsManagement.Infrastructure/
COPY src/Dwapi.ExtractsManagement.Infrastructure/Dwapi.ExtractsManagement.Infrastructure.csproj src/Dwapi.ExtractsManagement.Infrastructure/
COPY src/Dwapi.Contracts/Dwapi.Contracts.csproj src/Dwapi.Contracts/
RUN dotnet restore src/Dwapi/Dwapi.csproj
COPY . .
WORKDIR /src/src/Dwapi
RUN dotnet build Dwapi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Dwapi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dwapi.dll"]