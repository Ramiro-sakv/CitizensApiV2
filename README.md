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