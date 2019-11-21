# Build Environment
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /src

COPY *.sln ./
COPY lab.consumer/*.csproj ./lab.consumer/
COPY lab.domain/*.csproj ./lab.domain/
COPY lab.infrastructure.data/*.csproj ./lab.infrastructure.data/
COPY lab.infrastructure.ioc/*.csproj ./lab.infrastructure.ioc/
COPY lab.mq/*.csproj ./lab.mq/
COPY lab.service/*.csproj ./lab.service/
COPY lab.service.test/*.csproj ./lab.service.test/
COPY lab.webapi/*.csproj ./lab.webapi/
COPY lab.webapi.test/*.csproj ./lab.webapi.test/

RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o out

# Runtime Environment

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /src/out .
COPY --from=build-env /src/files/appsettings.json .
COPY --from=build-env /src/files/log4net.config .
COPY --from=build-env /src/files/localhost.pfx .

ENTRYPOINT ["dotnet", "lab.webapi.dll"]