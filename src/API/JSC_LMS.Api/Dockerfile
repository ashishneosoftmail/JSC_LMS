#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/API/JSC_LMS.Api/JSC_LMS.Api.csproj", "src/API/JSC_LMS.Api/"]
COPY ["src/Infrastructure/JSC_LMS.Infrastructure/JSC_LMS.Infrastructure.csproj", "src/Infrastructure/JSC_LMS.Infrastructure/"]
COPY ["src/Core/JSC_LMS.Application/JSC_LMS.Application.csproj", "src/Core/JSC_LMS.Application/"]
COPY ["src/Core/JSC_LMS.Domain/JSC_LMS.Domain.csproj", "src/Core/JSC_LMS.Domain/"]
COPY ["src/Infrastructure/JSC_LMS.Identity/JSC_LMS.Identity.csproj", "src/Infrastructure/JSC_LMS.Identity/"]
COPY ["src/Infrastructure/JSC_LMS.Persistence/JSC_LMS.Persistence.csproj", "src/Infrastructure/JSC_LMS.Persistence/"]
RUN dotnet restore "src/API/JSC_LMS.Api/JSC_LMS.Api.csproj"
COPY . .
WORKDIR "/src/src/API/JSC_LMS.Api"
RUN dotnet build "JSC_LMS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JSC_LMS.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JSC_LMS.Api.dll"]
