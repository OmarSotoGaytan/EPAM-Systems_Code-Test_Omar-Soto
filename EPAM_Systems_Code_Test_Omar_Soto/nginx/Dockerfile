# nginx/Dockerfile
FROM nginx:alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY .htpasswd /etc/nginx/.htpasswd
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]