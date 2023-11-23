#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ./BibliotecaApi/Biblioteca.Application/*.csproj ./BibliotecaApi/Biblioteca.Application/
COPY ./BibliotecaApi/Biblioteca.Domain/*.csproj ./BibliotecaApi/Biblioteca.Domain/
COPY ./BibliotecaApi/Biblioteca.Domain.Core/*.csproj ./BibliotecaApi/Biblioteca.Domain.Core/
COPY ./BibliotecaApi/Biblioteca.Infra.CrossCutting.Identity/*.csproj ./BibliotecaApi/Biblioteca.Infra.CrossCutting.Identity/
COPY ./BibliotecaApi/Biblioteca.Infra.CrossCutting.IoC/*.csproj ./BibliotecaApi/Biblioteca.Infra.CrossCutting.IoC/
COPY ./BibliotecaApi/Biblioteca.Infra.Data/*.csproj ./BibliotecaApi/Biblioteca.Infra.Data/
COPY ./BibliotecaApi/Biblioteca.TesteUnitario/*.csproj ./BibliotecaApi/Biblioteca.TesteUnitario/
COPY ./BibliotecaApi/Biblioteca/*.csproj ./BibliotecaApi/Biblioteca/
RUN dotnet restore ./BibliotecaApi/Biblioteca/Biblioteca.Api.csproj
COPY . .
WORKDIR "/src/BibliotecaApi/Biblioteca"
RUN dotnet build "Biblioteca.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Biblioteca.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Biblioteca.Api.dll"]


#