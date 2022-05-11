FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet restore 

COPY . ./
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/nightly/aspnet:6.0
WORKDIR /app
EXPOSE 5000

COPY --from=build /app/publish .
#ENTRYPOINT ["dotnet", "FastCreditChallenge.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet FastCreditChallenge.dll