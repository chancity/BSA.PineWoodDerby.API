FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["BSA.PineWoodDerby.API/BSA.PineWoodDerby.API.csproj", "BSA.PineWoodDerby.API/"]
RUN dotnet restore "BSA.PineWoodDerby.API/BSA.PineWoodDerby.API.csproj"
COPY . .
WORKDIR "/src/BSA.PineWoodDerby.API"
RUN dotnet build "BSA.PineWoodDerby.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BSA.PineWoodDerby.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BSA.PineWoodDerby.API.dll"]