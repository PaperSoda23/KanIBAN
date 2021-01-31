FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build

EXPOSE 80

WORKDIR /app/KanIBAN.API

COPY KanIBAN.API/KanIBAN.API.csproj .
RUN dotnet restore

COPY ./KanIBAN.API/ .
RUN dotnet publish -c Release -o out 


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS runtime

WORKDIR /app
COPY --from=build /app/KanIBAN.API/out ./

ENTRYPOINT ["dotnet", "KanIBAN.API.dll"]