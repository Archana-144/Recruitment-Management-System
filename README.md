# Recruitment Management System

A full-stack Recruitment Management System designed to simplify and streamline the hiring process by connecting recruiters, administrators, and job applicants through a centralized platform.

The application enables recruiters to manage job postings, track applications, review candidate profiles, and coordinate recruitment activities efficiently. Candidates can create profiles, explore job opportunities, apply for positions, and monitor their application status.

## 🚀 Features

### 👤 Candidate Module
- Candidate registration and login.
- Create and manage candidate profiles.
- Browse and search available job openings.
- Apply for suitable positions.
- Track application status.
- View application history.

### 🧑‍💼 Recruiter Module
- Recruiter registration and authentication.
- Create, update, and manage job postings.
- View candidates who applied for a position.
- Review candidate profiles and resumes.
- Shortlist or reject applications.
- Update recruitment progress.
- Track applications for each job opening.

### 🛡️ Admin Module
- Secure administrator login.
- Manage candidate and recruiter accounts.
- Monitor job postings.
- Manage job categories and departments.
- View recruitment statistics.
- Maintain platform integrity.

### 📋 Application Management
- Centralized candidate application tracking.
- Application status management.
- Status workflow: Applied → Under Review → Shortlisted → Interview Scheduled → Selected / Rejected.
- Prevent duplicate applications for the same job.
- Maintain application history.

### 🔍 Search and Filtering
- Search jobs by title or keywords.
- Filter jobs by department, location, and employment type.
- View relevant job details before applying.

## 🛠️ Technology Stack

The following is a suggested stack. Retain only the technologies actually used in your project.

**Frontend**
- React.js
- TypeScript
- HTML5
- CSS3

**Backend**
- ASP.NET Core Web API
- C#
- RESTful APIs

**Database**
- Microsoft SQL Server
- SQL Server Stored Procedures

**Development and Testing Tools**
- Visual Studio / Visual Studio Code
- SQL Server Management Studio (SSMS)
- Swagger / OpenAPI
- Postman
- Git and GitHub

## 🏗️ System Architecture

The application follows a layered architecture:

1. **Presentation Layer:** React-based user interface for candidates, recruiters, and administrators.
2. **API Layer:** ASP.NET Core Web API handles HTTP requests and responses.
3. **Business Logic Layer:** Validates requests and implements recruitment workflows.
4. **Data Access Layer:** Executes database operations using SQL queries, stored procedures, or the chosen ORM.
5. **Database Layer:** SQL Server stores users, job postings, candidate profiles, and application records.

### Request Flow

```text
User
  |
  v
React Frontend
  |
  v
ASP.NET Core Web API
  |
  v
Business Logic / Services
  |
  v
Data Access Layer
  |
  v
SQL Server Database
```

## 🗄️ Database Design

The following tables are proposed for the application.

| Table | Purpose |
|---|---|
| Users | Stores login credentials, roles, and account information |
| Candidates | Stores candidate profiles and professional details |
| Recruiters | Stores recruiter and company information |
| Jobs | Stores job descriptions, requirements, and employment details |
| Applications | Tracks candidate applications and their statuses |
| Interviews | Stores interview schedules and related details |
| Departments | Maintains department information |

### Relationships

- A candidate can apply for multiple jobs.
- A job can receive applications from multiple candidates.
- Each application connects a candidate with a job.
- A recruiter can create and manage multiple job postings.
- An application can have associated interview records.

## ⚙️ Installation and Setup

### Prerequisites

Install the following tools before running the application:

- Node.js and npm
- .NET SDK compatible with the project
- Microsoft SQL Server
- SQL Server Management Studio
- Git

### 1. Clone the Repository

```bash
git clone <your-repository-url>
cd RecruitmentManagementSystem
```

### 2. Set Up the Database

1. Open SQL Server Management Studio.
2. Create a database named `RecruitmentDB`.
3. Execute the database schema scripts.
4. Configure the database connection string in the backend configuration.

Example `appsettings.json` configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=RecruitmentDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER` with your SQL Server instance name.

Use appropriate authentication settings for your environment. Never commit production credentials or secrets to GitHub.

### 3. Run the Backend

Open a terminal in the backend project directory:

```bash
dotnet restore
dotnet build
dotnet run
```

Once the application starts, open its Swagger endpoint if Swagger is configured.

### 4. Run the Frontend

Open another terminal in the frontend project directory:

```bash
npm install
npm run dev
```

Open the local development URL printed by Vite in your terminal.

### 5. Configure API Connectivity

Set the frontend API base URL to match the backend URL.

Ensure that CORS is configured to allow requests from the frontend development origin.

## 🔐 Security Considerations

- Validate user input on the server.
- Store passwords using a secure password-hashing mechanism.
- Apply role-based authorization to protected endpoints.
- Prevent unauthorized access to recruiter and administrator features.
- Validate uploaded resume file types and sizes.
- Avoid exposing sensitive candidate information.
- Store secrets outside source control.
- Use HTTPS in production.

## 🧪 Testing

Test the application's main workflows using Swagger or Postman.

Recommended test scenarios:

- Candidate registration and login.
- Recruiter authentication and job creation.
- Job search and filtering.
- Successful job application submission.
- Prevention of duplicate applications.
- Application status updates.
- Unauthorized access to protected endpoints.
- Resume upload validation.
- Administrator account management.

## 🔮 Future Enhancements

- Email notifications for application updates.
- Interview scheduling and calendar integration.
- Resume parsing and candidate skill extraction.
- Candidate-job matching based on skills.
- Recruitment analytics dashboard.
- Pagination and advanced filtering.
- Automated testing and CI/CD integration.
- Cloud deployment.

## 💼 Resume Description

**Recruitment Management System | Full-Stack Development**

- Developed a recruitment management application to streamline job posting, candidate applications, and recruitment workflows.
- Designed RESTful APIs to manage candidate profiles, job openings, and application statuses.
- Designed a relational database schema in SQL Server to maintain candidate, recruiter, job, and application records.
- Implemented role-based access control for candidate, recruiter, and administrator workflows.
- Tested API endpoints and application workflows using Swagger and Postman.



## 👩‍💻 Author

**Your Name**

- GitHub: @Archana-144


## 📄 License

This project can be distributed under the MIT License if you choose to license it accordingly.
