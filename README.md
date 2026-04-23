# Movie Reservation System

A modern movie ticket booking management system built on .NET Core, applying Vertical Slice Architecture to optimize scalability and maintainability.

Inspired by: https://roadmap.sh/projects/movie-reservation-system

---

## APIs

The system provides a full set of RESTful APIs managed by FastEndpoints, organized into the following main groups:

* **Auth API**: Registration, Login (JWT), Role-based authorization (Admin/User).
* **Movies API**: Manage movie information, genres, and posters (S3).
* **Showtime API**: Manage showtimes, screening rooms, and schedule conflict validation.
* **Cinema Seat API**: Retrieve seat status and handle seat locking (booking).
* **Reservation API**: Manage orders and booking history.
* **Payment API**: Handle webhooks from the payment gateway to automate ticket confirmation.

---

## Features

* **Vertical Slice Architecture**: Implemented using the REPR pattern (Request-Endpoint-Response), improving code clarity and independence.
* **Real-time Seat Locking**: Temporarily holds seats while users complete payment.
* **Automated Background Services**:

  * **SeatCleanupService**: Automatically releases seats after 15 minutes if payment is not completed.
  * **SendEmailService**: Background worker that processes email queue and sends tickets to customers.
* **Payment Integration**: Integrated SePay webhook with smart Regex logic to identify transaction codes from bank transfers.
* **Cloud Asset Management**: Store and manage movie images using Supabase S3 Storage.

---

## Tech Stack

* **Backend**: .NET 10, FastEndpoints, FluentValidation, Ardalis.Specification
* **Database**: PostgreSQL, Entity Framework Core (Code First)
* **Security**: JWT Authentication, User Secrets Management
* **Infrastructure**: Ngrok (Webhook Tunneling), Supabase (S3), Mailtrap (SMTP Testing)

---

## Installation

### Configuration

The project uses User Secrets to store sensitive data. Configure the following:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=MovieDb;Username=postgres;Password=xxx"
dotnet user-secrets set "Jwt:Key" "your-secret-key"
dotnet user-secrets set "S3Storage:AccessKey" "supabase-s3-access-key"
dotnet user-secrets set "SeQRPay:WebhookToken" "token-from-sepay"
```

---

## Usage (Local Development)

### Run Database

Make sure PostgreSQL is running and apply migrations:

```bash
dotnet ef database update
```

### Setup Webhook Tunnel

Use Ngrok to receive callbacks from SePay to your localhost:

```bash
ngrok http https://localhost:5001
```


### Run Application

```bash
dotnet run
```

---

## Project Structure

```plaintext
movie-reservation-system/
├── Features/
│   ├── Auth/
│   ├── Movies/
│   ├── Showtime/
│   ├── Seats/
│   └── Payment/
├── Infrastructure/
├── Model/
├── Config/
├── Dto/
├── Exception/
└── Extensions/
```
