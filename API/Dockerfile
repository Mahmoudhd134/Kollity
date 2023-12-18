FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy all the layers' csproj files into respective folders
COPY ["./API/API.csproj", "src/API/"]
COPY ["./Application/Application.csproj", "src/Application/"]
COPY ["./Domain/Domain.csproj", "src/Domain/"]
COPY ["./Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["./Persistence/Persistence.csproj", "src/Persistence/"]

ADD fileToBreakTheCache.txt /app/fileToBreakTheCache.txt

# run restore over API project - this pulls restore over the dependent projects as well
RUN dotnet restore "src/API/API.csproj"

COPY . .

# run build over the API project
WORKDIR "/src/API/"
RUN dotnet build -c Release -o /app/build

# run publish over the API project
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime
WORKDIR /app

COPY --from=publish /app/publish .
RUN ls -l
ENTRYPOINT [ "dotnet", "API.dll" ]