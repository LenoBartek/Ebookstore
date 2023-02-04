#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Ebookstore/Ebookstore.csproj", "Ebookstore/"]
RUN dotnet restore "Ebookstore/Ebookstore.csproj"
COPY . .
WORKDIR "/src/Ebookstore"
RUN dotnet build "Ebookstore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ebookstore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ebookstore.dll"]