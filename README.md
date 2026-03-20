# CitizensApiV2

## Description
CitizensApiV2 is an ASP.NET Core Web API that manages citizens in a city registry system.
It supports CRUD operations, integrates with an external API to assign a random personal asset, stores data in a CSV file, and exposes Swagger UI for testing.

## Features
- Create Citizen
- Get All Citizens
- Get Citizen by CI
- Update Citizen by CI
- Delete Citizen by CI
- Random BloodGroup assignment
- Random PersonalAsset assignment from external API
- CSV file persistence
- Swagger UI
- Logging with Serilog

## Technologies
- ASP.NET Core Web API
- C#
- Swagger / OpenAPI
- Serilog
- CSV file storage
- Git / GitHub

## Project Structure
- Controllers
- DTOs
- Models
- Repositories
- Services
- Data

## Endpoints

### GET /api/Citizen
Returns all citizens.

### GET /api/Citizen/{ci}
Returns the citizen matching the provided CI.

### POST /api/Citizen
Creates a citizen.

Example body:
```json
{
  "firstName": "Ramiro",
  "lastName": "Huarachi",
  "ci": "13067264"
}
PUT /api/Citizen/{ci}

Updates only first name and last name.

Example body:

{
  "firstName": "Ramiro Updated",
  "lastName": "Huarachi Updated"
}
DELETE /api/Citizen/{ci}

Deletes a citizen by CI.

Data Persistence

The application stores citizen data in a CSV file located at:

Data/citizens.csv

The file is created automatically if it does not exist.
The repository reads and writes data to this file in order to persist information between executions.

External API

The project uses the following external API to retrieve random objects:

https://api.restful-api.dev/objects

One random object is selected and assigned as the citizen's PersonalAsset during creation.

Swagger

Swagger UI is enabled so the API can be tested directly from the browser.

Typical local URL:

http://localhost:5260/swagger

Logging

Logging is implemented using Serilog.

The application logs:

citizen creation

citizen update

citizen deletion

external API calls

CSV file errors

How to Run

Run the following commands:

dotnet restore
dotnet build
dotnet run

Then open Swagger in the browser.

Git Flow

The project uses the following branches:

main

develop

P2-001

Development was performed in P2-001, then integrated into develop, and finally merged into main.

Twelve-Factor App
1. Codebase

The application uses a single codebase tracked in Git and hosted on GitHub.

2. Dependencies

Dependencies are explicitly declared in the .csproj file and restored with NuGet.

3. Config

Configuration values such as the CSV file path and external API URL are stored in appsettings.json.

4. Backing Services

The external objects API is treated as an attached backing service.

5. Build, Release, Run

The application can be built with dotnet build, versioned through Git, and executed with dotnet run.

6. Processes

The application is stateless. Persistent data is stored in the CSV file instead of memory.

7. Port Binding

The API is exposed through an HTTP port and is accessible through Swagger.

8. Concurrency

The project does not implement multiple process types, but it could scale by running multiple instances.

9. Disposability

The application starts quickly and can be stopped gracefully.

10. Dev / Prod Parity

The same configuration style and dependencies can be maintained across environments.

11. Logs

Logs are treated as event records for system behavior and errors.

12. Admin Processes

Administrative tasks such as reviewing or cleaning the CSV file can be executed separately from the main process.

Author

Ramiro