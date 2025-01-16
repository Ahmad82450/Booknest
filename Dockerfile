# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file and all project files
COPY ./BooknestAPI/BooknestAPI.sln .
COPY ./BLL/*.csproj ./BLL/
COPY ./DAL/*.csproj ./DAL/
COPY ./CL/*.csproj ./CL/
COPY ./UnitTests_BE/*.csproj ./UnitTests_BE/
COPY ./BooknestAPI/*.csproj ./BooknestAPI/

# Restore dependencies
RUN dotnet restore "./BooknestAPI/BooknestAPI.csproj" --disable-parallel

# Copy all the source files
COPY . ./

# Publish the application
WORKDIR /app/BooknestAPI
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/BooknestAPI/out .

# Expose the port the app runs on
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "BooknestAPI.dll"]
