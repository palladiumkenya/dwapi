FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Dwapi/Dwapi.csproj Dwapi/
COPY Dwapi.SharedKernel/Dwapi.SharedKernel.csproj Dwapi.SharedKernel/
COPY Dwapi.SettingsManagement.Core/Dwapi.SettingsManagement.Core.csproj Dwapi.SettingsManagement.Core/
COPY Dwapi.SharedKernel.Infrastructure/Dwapi.SharedKernel.Infrastructure.csproj Dwapi.SharedKernel.Infrastructure/
COPY Dwapi.UploadManagement.Infrastructure/Dwapi.UploadManagement.Infrastructure.csproj Dwapi.UploadManagement.Infrastructure/
COPY Dwapi.UploadManagement.Core/Dwapi.UploadManagement.Core.csproj Dwapi.UploadManagement.Core/
COPY Dwapi.ExtractsManagement.Core/Dwapi.ExtractsManagement.Core.csproj Dwapi.ExtractsManagement.Core/
COPY Dwapi.SettingsManagement.Infrastructure/Dwapi.SettingsManagement.Infrastructure.csproj Dwapi.SettingsManagement.Infrastructure/
COPY Dwapi.ExtractsManagement.Infrastructure/Dwapi.ExtractsManagement.Infrastructure.csproj Dwapi.ExtractsManagement.Infrastructure/
RUN dotnet restore Dwapi/Dwapi.csproj
COPY . .
WORKDIR /src/Dwapi
RUN dotnet build Dwapi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Dwapi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dwapi.dll"]
