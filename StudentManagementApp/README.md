# Student Management Application

This is a simple Student Management Application built using ASP.NET Core. The application allows users to manage student records, including creating, editing, and viewing student details.

## Project Structure

The project consists of the following main components:

- **Controllers**: Contains the `StudentController` which handles HTTP requests related to student management.
- **Models**: Contains the `Student` model that defines the properties of a student.
- **Views**: Contains Razor views for displaying and managing student data.
  - **Student**: Contains views for listing, creating, and editing students.
  - **Shared**: Contains shared layout views.
- **wwwroot**: Contains static files such as CSS.
- **Configuration Files**: Includes `appsettings.json` for application settings, `Program.cs` for the application entry point, and `Startup.cs` for configuring services and middleware.

## Features

- View a list of students.
- Create a new student record.
- Edit existing student records.
- Responsive design with CSS for better user experience.

## Setup Instructions

1. Clone the repository:
   ```
   git clone <repository-url>
   ```

2. Navigate to the project directory:
   ```
   cd StudentManagementApp
   ```

3. Restore the dependencies:
   ```
   dotnet restore
   ```

4. Run the application:
   ```
   dotnet run
   ```

5. Open your web browser and navigate to `http://localhost:5000` to access the application.

## Usage

- Use the navigation links to access different functionalities of the application.
- Follow the prompts to create or edit student records.

## Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue for any suggestions or improvements.