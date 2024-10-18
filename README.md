# EPAM-Systems_Code-Test_Omar-Soto
# Project Overview

## BACKEND
- The **.NET 8 API template** was used for the backend.
- The **SignalR library** was implemented for real-time data transmission.
- The architecture follows the **Clean Architecture** pattern, which consists of three layers: **Domain**, **Application**, and **Infrastructure**.

### Domain Layer
- The **Domain** layer contains classes and interfaces. In this case, there is only one interface.

### Application Layer
- The **Application** layer holds the business logic, which, in this case, corresponds to the **TextProcessor**.

### Infrastructure Layer
- The **Infrastructure** layer serves as the integration point for external libraries that are outside the business logic.
### Testing
- **xUnit** and **Moq** were utilized for unit testing.
## FRONTEND
- The frontend was developed using **React** with **Vite**.
- It is important to run the command `npm install` to install the necessary dependencies.

### Environment Configuration
- You need to modify the `.env` file to include the URL where the application is running. In this case, my `.env.local` file looks like this:
- VITE_SIGNALR_URL=https://localhost:7048/
-
### Libraries Used
-  **Tailwind CSS**  to simplify the UI development, tailwind was integrated for styling.
- **React-Toastify** was included for displaying notifications easily.
### Project Structure
- The project is divided into several folders:
- **components**: This folder contains reusable code pieces.
- **pages**: This folder includes the pages of the application.
- **hooks**: This folder contains logic that can be reused and injected, similar to a service in Angular. In this case, I added a hook to manage SignalR events.
