FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DiceProbCalc/DiceProbCalc.csproj", "DiceProbCalc/"]
RUN dotnet restore "DiceProbCalc/DiceProbCalc.csproj"
COPY . .
WORKDIR "/src/DiceProbCalc"
RUN dotnet build "DiceProbCalc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiceProbCalc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiceProbCalc.dll"]
