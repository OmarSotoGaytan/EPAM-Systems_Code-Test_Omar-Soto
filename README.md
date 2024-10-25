# EPAM-Systems_Code-Test_Omar-Soto
# Project Overview
[screen-capture (1).webm](https://github.com/user-attachments/assets/6a48fdf2-7f24-4a9e-8b3b-9730f6e163d6)

## Disclaimers

- I pushed all changes directly to the **main** branch. I understand that this is not a best practice in a real work environment, but I did it here for the sake of practicality.
- I noticed that the commits were made with a secondary test Git account.
- The code now covers  **Part 1**  and **Part 2** of the coding task that was requested.
- The **.NET 8 API with React template** was used for the whole solution.


## BACKEND
- The **.NET 8 API template** was used for the backend.
- The **SignalR library** was implemented for real-time data transmission.
- The architecture follows the **Clean Architecture** pattern, which consists of three layers: **Domain**, **Application**, and **Infrastructure**.

### Domain Layer
- The **Domain** layer contains classes and interfaces. In this case, there is only one interface.

### Application Layer
- The **Application** layer holds the business logic, which, in this case, corresponds to the **TextProcessor**.

### Infrastructure Layer
- The **Infrastructure** layer serves as the integration point for external libraries that are outside the business logic, in this case the one i'm using it's **signalr**.
### Testing
- A separate project was created specifically for the unit tests to maintain a clear structure and organization.
- **xUnit** and **Moq** were utilized for unit testing.
## FRONTEND
- The frontend was developed using **React** with **Vite**.
- It is important to run the command `npm install` to install the necessary dependencies.

### Environment Configuration
- You need to modify the `.env` file to include the URL where the application back-end is running. In this case, my `.env.local` file looks like this:
- VITE_SIGNALR_URL=
- VITE_USER=
- VITE_PASSWORD=
### Libraries Used
-  **Tailwind CSS**  to simplify the UI development, tailwind was integrated for styling.
- **React-Toastify** was included for displaying notifications easily.
### Project Structure
- The project is divided into several folders:
- **components**: This folder contains reusable code pieces.
- **pages**: This folder includes the pages of the application.
- **hooks**: This folder contains logic that can be reused and injected, similar to a service in Angular. In this case, I added a hook to manage SignalR events.

## DOCKER AND CONTAINERS
- **Docker** and **Docker Compose** were used to manage the application's containers and ensure smooth deployment.

### Containers Overview
The task required three containers, each serving a specific purpose:

1. **Frontend**:  
   - Hosts the UI built with React and served via Vite.
   - Communicates with the backend through the proxy.

2. **Backend**:  
   - Supports real-time communication using SignalR.

3. **Nginx**:  
   - Acts as a reverse proxy, routing traffic to the frontend and backend.
   - Secures the proxy layer using **Basic Authentication** to control access and send request to the backend.

### Running the Containers
1. Make sure you have **Docker** and **Docker Compose** installed on your machine.
2. Use the following command where the docker-componse.yml is located to build and run the containers:
   ```bash
   docker-compose up --build