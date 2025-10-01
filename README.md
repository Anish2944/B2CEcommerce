# B2CEcommerce

## 📖 Overview
B2CEcommerce is an ASP.NET Core MVC web application designed to provide a scalable, secure, and modular foundation for building modern e-commerce platforms. The project follows MVC architecture, supports user authentication/authorization, product management, shopping cart functionality, and order processing.

---

## 🚀 Features
- **ASP.NET Core MVC** architecture for maintainable and testable code.
- **Entity Framework Core** integration for database operations.
- **Authentication & Authorization** using Identity.
- **Product Catalog** with category and filtering support.
- **Shopping Cart** with session-based persistence.
- **Order Management** for checkout and history tracking.
- **Responsive UI** using Bootstrap.
- **Dependency Injection** and **Configuration Management**.
- **Logging & Error Handling** with built-in middleware.

---

## 🛠️ Tech Stack
- **Frontend:** Razor Views, Bootstrap, jQuery  
- **Backend:** ASP.NET Core MVC 7.0  
- **Database:** SQL Server (via Entity Framework Core)  
- **Authentication:** ASP.NET Core Identity  
- **Other Tools:** LINQ, Dependency Injection, Logging  

---

## 📂 Project Structure
```
B2CEcommerce/
│-- Controllers/      # MVC controllers
│-- Models/           # Entity models and view models
│-- Views/            # Razor views
│-- Data/             # DbContext and migrations
│-- wwwroot/          # Static assets (CSS, JS, images)
│-- Services/         # Business logic and helper services
│-- Program.cs        # Application entry point
│-- Startup.cs        # Middleware and service configuration
```

---

## ⚙️ Getting Started

### Prerequisites
- .NET 7.0 SDK  
- SQL Server / LocalDB  
- Visual Studio 2022 or VS Code  

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/Anish2944/B2CEcommerce.git
   cd B2CEcommerce
   ```
2. Update database connection string in `appsettings.json`.
3. Run migrations and update the database:
   ```bash
   dotnet ef database update
   ```
4. Build and run the application:
   ```bash
   dotnet run
   ```

### Running in Visual Studio
- Open `B2CEcommerce.sln`  
- Set startup project to **B2CEcommerce**  
- Press `F5` to run  

---

## 📦 Database
- Entity Framework Core is used for ORM.
- Run migrations with:
  ```bash
  dotnet ef migrations add <MigrationName>
  dotnet ef database update
  ```

---

## ✅ Testing
- Unit tests are located in the `B2CEcommerce.Tests` project.  
- Run tests using:
  ```bash
  dotnet test
  ```

---

## 🤝 Contributing
1. Fork the repo  
2. Create a feature branch (`git checkout -b feature/YourFeature`)  
3. Commit changes (`git commit -m 'Add some feature'`)  
4. Push to branch (`git push origin feature/YourFeature`)  
5. Open a Pull Request  

---

## 📜 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.  
