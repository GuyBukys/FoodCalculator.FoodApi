#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FoodCalculator.FoodApi/FoodCalculator.FoodApi.csproj", "FoodCalculator.FoodApi/"]
RUN dotnet restore "FoodCalculator.FoodApi/FoodCalculator.FoodApi.csproj"
COPY . .
WORKDIR "/src/FoodCalculator.FoodApi"
RUN dotnet build "FoodCalculator.FoodApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodCalculator.FoodApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodCalculator.FoodApi.dll"]