# build environment
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
WORKDIR /app/Finpe.Api
RUN dotnet publish -c Release -o out

# production environment
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
ARG COMMIT
ENV APP_VERSION $COMMIT
COPY --from=build /app/Finpe.Api/out ./
ENTRYPOINT ["dotnet", "Finpe.Api.dll"]