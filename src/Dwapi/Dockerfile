FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5757 5753

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ./Dwapi/Dwapi.csproj Dwapi/
COPY src/Dwapi.SharedKernel/Dwapi.SharedKernel.csproj Dwapi.SharedKernel/
COPY src/Dwapi.SettingsManagement.Core/Dwapi.SettingsManagement.Core.csproj Dwapi.SettingsManagement.Core/
COPY src/Dwapi.SharedKernel.Infrastructure/Dwapi.SharedKernel.Infrastructure.csproj Dwapi.SharedKernel.Infrastructure/
COPY src/Dwapi.UploadManagement.Infrastructure/Dwapi.UploadManagement.Infrastructure.csproj Dwapi.UploadManagement.Infrastructure/
COPY src/Dwapi.UploadManagement.Core/Dwapi.UploadManagement.Core.csproj Dwapi.UploadManagement.Core/
COPY src/Dwapi.ExtractsManagement.Core/Dwapi.ExtractsManagement.Core.csproj Dwapi.ExtractsManagement.Core/
COPY src/Dwapi.SettingsManagement.Infrastructure/Dwapi.SettingsManagement.Infrastructure.csproj Dwapi.SettingsManagement.Infrastructure/
COPY src/Dwapi.ExtractsManagement.Infrastructure/Dwapi.ExtractsManagement.Infrastructure.csproj Dwapi.ExtractsManagement.Infrastructure/
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
