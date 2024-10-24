# Etapa 1: Construcción de la app
FROM node:18-alpine AS builder

# Establece el directorio de trabajo dentro del contenedor
WORKDIR /app

# Copia los archivos necesarios
COPY package*.json ./
COPY vite.config.* ./

# Instala las dependencias
RUN npm install

# Copia el resto del código fuente
COPY . .

# Compila la aplicación
RUN npm run build

# Etapa 2: Servir la app usando un servidor web (nginx)
FROM nginx:alpine

# Copia los archivos compilados al directorio de nginx
COPY --from=builder /app/dist /usr/share/nginx/html

# Expone el puerto que usará nginx
EXPOSE 80

# Inicia nginx
CMD ["nginx", "-g", "daemon off;"]