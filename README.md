# Sport Radar Assessment

This project was created as part of a recruitment assessment.  
The main focus of the project is the backend implementation.

## 🛠 Tech Stack

**Backend:**
- ASP.NET Core (C#)
- PostgreSQL

**Frontend:**
- Angular

## 🚀 Backend Implementation

The backend was built using ASP.NET Core Web API and includes:

- PostgreSQL database connection configured via `.env`
- CORS configuration to enable communication with the frontend
- REST API controller for:
  - retrieving events
  - creating new events
- Entity models used for database structure
- DTOs (Data Transfer Objects) used for communication between backend and frontend

## 🎨 Frontend Implementation

The frontend was built using Angular and includes:

- DTO interfaces for handling data from the backend
- Services for managing API endpoints and HTTP communication
- Routing between pages
- Three main views:
  - **Show Events**
    - Displays a list of all events retrieved from the database
    - Allows the user to enter an event ID in a form to navigate to the single event view
  - **Create Event**
    - Provides a form for creating a new event
    - After submission, redirects to the events view
  - **Single Event**
    - Displays detailed information about a selected event

## ⚙️ Requirements

### Backend
- .NET SDK 10.0  
  [Download .NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

### Frontend
- Node.js 18+  
  [Download Node.js](https://nodejs.org/)  
- npm 9+  
- Angular CLI 16+  
  ```bash
  npm install -g @angular/cli@16

### ▶️ How to Run

## Environment Setup
- Create a new .env file based on .env.example
- Fill in the required values to enable database migrations and data retrieval
- Run the following commands to apply database migrations:
```bash 
dotnet ef migrations add InitialCreate
dotnet ef database update

## Backend and Frontend
```bash 
cd Backend/Backend
dotnet run

cd Frontend/front
npm start
