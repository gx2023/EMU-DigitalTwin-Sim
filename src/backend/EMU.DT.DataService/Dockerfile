FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EMU.DT.DataService.csproj", "."]
RUN dotnet restore "EMU.DT.DataService.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "EMU.DT.DataService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EMU.DT.DataService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EMU.DT.DataService.dll"]
