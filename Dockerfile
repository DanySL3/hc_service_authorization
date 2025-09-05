FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

RUN sed -i 's/TLSv1.2/TLSv1/g' /etc/ssl/openssl.cnf
ENV TZ="America/Lima"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["InfrastructureCoreDatabase/InfrastructureCoreDatabase.csproj", "InfrastructureCoreDatabase/"]
COPY ["InfrastructureLogDatabase/InfrastructureLogDatabase.csproj", "InfrastructureLogDatabase/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["WorkerService/WorkerService.csproj", "WorkerService/"]
COPY ["TestUnitApi/TestUnitApi.csproj", "TestUnitApi/"]
COPY ["Api/Api.csproj", "Api/"]

RUN dotnet restore "Api/Api.csproj"

COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]