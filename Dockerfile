# ---------- build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ---------- runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# copy published output
COPY --from=build /app/publish .

# expose port (ASP.NET default inside container)
EXPOSE 8080

# set environment
ENV ASPNETCORE_URLS=http://+:8080

# run the app
ENTRYPOINT ["dotnet", "DSVMeetingRoomBooking.dll"]
