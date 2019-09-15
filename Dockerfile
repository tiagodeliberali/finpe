FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Finpe/*.csproj ./Finpe/
COPY Finpe.Api/*.csproj ./Finpe.Api/
RUN dotnet restore

# copy everything else and build app
COPY Finpe/. ./Finpe/
COPY Finpe.Api/. ./Finpe.Api/
WORKDIR /app/Finpe.Api
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/Finpe.Api/out ./
ENTRYPOINT ["dotnet", "aspnetapp.dll"]