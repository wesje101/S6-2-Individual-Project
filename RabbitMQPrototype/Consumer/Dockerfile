FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=Development
ENV Logging__Console__FormatterName=simple

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Consumer.csproj", "./"]
RUN dotnet restore "Consumer.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Consumer.dll"]
