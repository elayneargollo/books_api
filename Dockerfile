FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["BookStoreApi/Solutis.csproj", "BookStoreApi/"]
RUN dotnet restore "BookStoreApi/Solutis.csproj"
COPY ./BookStoreApi ./BookStoreApi
WORKDIR "/src/BookStoreApi"
RUN dotnet build "Solutis.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Solutis.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Solutis.dll"]

#RUN useradd -m myappuser
#USER myappuser

##CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Solutis.dll
