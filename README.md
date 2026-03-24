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
- Two main views:
  - **Show Events**
    - displays all events or a selected event
  - **Create Event**
    - form for creating a new event
    - after submission, redirects to the events view

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

## Backend and Frontend
```bash 
cd Backend/Backend
dotnet run

cd Frontend/front
npm start
