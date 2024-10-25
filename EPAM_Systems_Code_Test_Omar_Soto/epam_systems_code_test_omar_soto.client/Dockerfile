# Use a lightweight Node.js 18 image as the base to build the app
FROM node:18-alpine AS builder

# Set the working directory inside the container
WORKDIR /app

# Copy the necessary files
COPY package*.json ./
COPY vite.config.* ./

# Install dependencies
RUN npm install

# Copy the rest of the source code
COPY . .

# Build the application
RUN npm run build

# Stage 2: Serve the app using a web server (nginx)
FROM nginx:alpine

# Copy the built files to the nginx directory
COPY --from=builder /app/dist /usr/share/nginx/html

# Expose the port that nginx will use
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]