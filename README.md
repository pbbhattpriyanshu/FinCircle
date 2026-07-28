# FinCircle - Next-Gen Financial Circle Management System

![FinCircle Banner](https://img.shields.io/badge/FinCircle-Enterprise%20Finance-FACC15?style=for-the-badge&logo=shield-check&logoColor=090D16)
![.NET](https://img.shields.io/badge/ASP.NET%20Core-10.0-512BD4?style=flat-square&logo=dotnet)
![Angular](https://img.shields.io/badge/Angular-Standalone-DD0031?style=flat-square&logo=angular)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Neon%20DB-4169E1?style=flat-square&logo=postgresql)
![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)

**FinCircle** is a modern, enterprise-grade Financial Circle & Savings Committee Management Platform inspired by real-world community savings funds and ROSCAs (Rotating Savings and Credit Associations). It streamlines group capital pool management, member directory tracking, loan issuance & approval workflows, and deposit logs through real-time financial telemetry.

---

## 🌟 Key Features & Modules

### 🔐 1. Authentication & Security
- **JWT Bearer Token Authentication**: Secure token-based access across API endpoints.
- **BCrypt Password Encryption**: Strong hashing algorithm for user passwords.
- **Role-Based Access Control**: Differentiated permissions for Admins and Committee Members.

### 📊 2. Live Ecosystem Telemetry (Dashboard)
- **Real-Time Financial Overview**: Total Pool Capital, Active Membership, Loan Applications, and System Health.
- **Interactive Quick-Action Shortcuts**: Instant navigation to Member Management, Loan Portfolio, and Contribution Logs.

### 👥 3. Member Directory (Full CRUD)
- **Roster Management**: View, register, update, and delete committee members.
- **Member Identifiers**: Auto-generated unique Member Codes (`FCXXXXXX`) and avatar profile initials.
- **Client-Side Live Filtering**: Instant search by member name, code, email, or phone number.

### 🏦 4. Loan Portfolio & Credit Lines
- **Loan Application System**: Member loan requests with interest rate, duration, and purpose tracking.
- **Approval & Rejection Workflow**: One-click approval (`POST /api/loan/{id}/approve`) and rejection handlers.
- **EMI & Repayment Calculator**: Dynamic monthly payment previews inside the UI.

### 🐖 5. Contributions & Deposit Log
- **Group Capital Pool Recording**: Log monthly deposit contributions per member.
- **Payment Method Tracking**: Support for Bank Transfer, Cash, UPI, Credit Card, and Cheque with transaction reference notes.
- **Yearly & Monthly Analytics**: Year filtering and accumulated pool volume metrics.

---

## 🏗️ System Architecture & Technology Stack

```
           +---------------------------------------------------+
           |            FinCircle Angular 19 UI                |
           |   (Standalone Components, Bootstrap 5, RxJS)      |
           +-------------------------+-------------------------+
                                     |
                                HTTP / REST
                              (Bearer Token)
                                     |
           +-------------------------v-------------------------+
           |          FinCircle ASP.NET Core 10 API            |
           |     (Controllers, Services, Repositories)         |
           +-------------------------+-------------------------+
                                     |
                                  EF Core
                                     |
           +-------------------------v-------------------------+
           |         PostgreSQL Database (Neon Serverless)     |
           +---------------------------------------------------+
```

### Backend (`FinCircle.API`)
- **Framework**: ASP.NET Core 10 Web API
- **ORM**: Entity Framework Core 10 (EF Core)
- **Database**: PostgreSQL (Hosted on Neon Serverless Postgres via Npgsql)
- **Authentication**: JWT Bearer Tokens (`Microsoft.AspNetCore.Authentication.JwtBearer`)
- **Security**: BCrypt Password Hashing (`BCrypt.Net-Next`)
- **Documentation**: Swagger OpenAPI (`Swashbuckle.AspNetCore`)

### Frontend (`FinCircle.UI`)
- **Framework**: Angular 19 (Standalone Components Architecture)
- **Styling**: Vanilla CSS with custom Dark Mode Finance Theme & Bootstrap 5
- **Icons**: Bootstrap Icons (`bi-*`)
- **State & HTTP**: RxJS `HttpClient` with Bearer Auth Token headers

---

## 📡 REST API Endpoints

### 🔑 Authentication (`/api/auth`)
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `POST` | `/api/auth/register` | Register a new user account |
| `POST` | `/api/auth/login` | Authenticate user & return JWT token |

### 📊 Telemetry (`/api/dashboard`)
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/dashboard` | Fetch aggregated pool capital, loans, and member telemetry |

### 👥 Member Directory (`/api/member`)
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/member` | List all circle members |
| `GET` | `/api/member/{id}` | Get specific member details by ID |
| `POST` | `/api/member` | Create a new circle member |
| `PUT` | `/api/member/{id}` | Update member details |
| `DELETE` | `/api/member/{id}` | Delete member record |

### 🏦 Loan Portfolio (`/api/loan`)
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/loan` | List all loan applications |
| `GET` | `/api/loan/{id}` | Get loan details by ID |
| `POST` | `/api/loan` | Submit new loan request |
| `POST` | `/api/loan/{id}/approve` | Approve loan application |
| `POST` | `/api/loan/{id}/reject` | Reject loan application |

### 🐖 Contributions Log (`/api/contribution`)
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/contribution` | List all member contribution records |
| `GET` | `/api/contribution/{id}` | Get contribution receipt details by ID |
| `POST` | `/api/contribution` | Record new monthly pool deposit |

---

## ⚙️ Getting Started & Setup Guide

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (v18+ or v20+) & NPM
- [PostgreSQL](https://www.postgresql.org/) (or cloud database connection string)

### 1. Clone the Repository
```bash
git clone https://github.com/pbbhattpriyanshu/FinCircle.git
cd FinCircle
```

### 2. Backend Setup (`FinCircle.API`)
```bash
cd FinCircle.API

# Restore dependencies
dotnet restore

# Run database migrations
dotnet ef database update

# Start the ASP.NET Core API server
dotnet run
```
The API backend will start on **`https://localhost:7070`** (Swagger UI available at `https://localhost:7070/swagger`).

### 3. Frontend Setup (`FinCircle.UI`)
```bash
cd ../FinCircle.UI

# Install packages
npm install

# Launch Angular development server
npm start
```
Open your browser and navigate to **`http://localhost:4200/`**.

---

## 🛡️ Database Configuration (`appsettings.json`)
The API uses Entity Framework Core configured with PostgreSQL in `FinCircle.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=<your-postgres-host>; Database=FinCircleDB; Username=<username>; Password=<password>; SSL Mode=VerifyFull;"
  },
  "Jwt": {
    "Key": "<your-strong-jwt-secret-key>",
    "Issuer": "FinCircle",
    "Audience": "FinCircleUsers"
  }
}
```

---

## 📄 License
This project is licensed under the [MIT License](LICENSE).

