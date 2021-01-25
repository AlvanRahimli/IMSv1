FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["IMSv1.csproj", "."]

RUN dotnet restore "IMSv1.csproj"
COPY . /src

RUN dotnet build "IMSv1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IMSv1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IMSv1.dll"]