#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bcm.Aed.FormulaOne.WebAPI/Bcm.Aed.FormulaOne.WebAPI.csproj", "Bcm.Aed.FormulaOne.WebAPI/"]
COPY ["Bcm.Aed.FormulaOne.Model/Bcm.Aed.FormulaOne.Model.csproj", "Bcm.Aed.FormulaOne.Model/"]
RUN dotnet restore "./Bcm.Aed.FormulaOne.WebAPI/./Bcm.Aed.FormulaOne.WebAPI.csproj"
COPY . .
WORKDIR "/src/Bcm.Aed.FormulaOne.WebAPI"
RUN dotnet build "./Bcm.Aed.FormulaOne.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Bcm.Aed.FormulaOne.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bcm.Aed.FormulaOne.WebAPI.dll"]