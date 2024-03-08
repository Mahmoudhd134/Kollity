FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy all the layers' csproj files into respective folders
COPY ["./Kollity.API/Kollity.API.csproj", "src/Kollity.API/"]
COPY ["./Kollity.Application/Kollity.Application.csproj", "src/Kollity.Application/"]
COPY ["./Kollity.Domain/Kollity.Domain.csproj", "src/Kollity.Domain/"]
COPY ["./Kollity.Infrastructure/Kollity.Infrastructure.csproj", "src/Kollity.Infrastructure/"]
COPY ["./Kollity.Persistence/Kollity.Persistence.csproj", "src/Kollity.Persistence/"]

# run restore over API project - this pulls restore over the dependent projects as well
RUN dotnet restore "src/Kollity.API/Kollity.API.csproj"

COPY . .

# run build over the API project
WORKDIR "/src/Kollity.API/"
RUN dotnet build -c Release -o /app/build

# run publish over the API project
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Kollity.API.dll" ]
