FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=Development
ENV Logging__Console__FormatterName=simple

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NewTask.csproj", "./"]
RUN dotnet restore "NewTask.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "NewTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewTask.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewTask.dll"]
