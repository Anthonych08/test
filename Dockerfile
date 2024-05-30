FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update -y && apt-get install libfontconfig -y
RUN apt-get install -y libgdiplus libc6 libc6-dev
RUN apt-get install -y libharfbuzz0b libfreetype6
RUN apt-get update -y && apt-get install -y libfontconfig1

RUN apt-get install -y wget
RUN wget -q https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
RUN apt-get install -y ./google-chrome-stable_current_amd64.deb

# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .

RUN dotnet restore "./4_InspectionBackEnd_Api/4_InspectionBackEnd_Api.csproj" --disable-parallel
RUN dotnet publish "./4_InspectionBackEnd_Api/4_InspectionBackEnd_Api.csproj" -c release -o /app --no-restore
EXPOSE 443
 

FROM base AS final
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "4_InspectionBackEnd_Api.dll"]