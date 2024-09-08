#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ExcelFileUploadApi.Presentation/ExcelFileUploadApi.Presentation.csproj", "ExcelFileUploadApi.Presentation/"]
RUN dotnet restore "./ExcelFileUploadApi.Presentation/ExcelFileUploadApi.Presentation.csproj"
COPY . .
WORKDIR "/src/ExcelFileUploadApi.Presentation"
RUN dotnet build "./ExcelFileUploadApi.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ExcelFileUploadApi.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExcelFileUploadApi.Presentation.dll"]