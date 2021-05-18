#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CountryHolidays/CountryHolidays.csproj", "CountryHolidays/"]
RUN dotnet restore "CountryHolidays/CountryHolidays.csproj"
COPY . .
WORKDIR "/src/CountryHolidays"
RUN dotnet build "CountryHolidays.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountryHolidays.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountryHolidays.dll"]