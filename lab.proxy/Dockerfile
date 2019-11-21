FROM nginx:alpine
EXPOSE 80
COPY lab.proxy/content /usr/share/nginx/html
COPY files/localhost.pfx /etc/nginx/ssl/localhost.pfx
RUN rm /etc/nginx/nginx.conf
COPY lab.proxy/nginx.conf /etc/nginx/nginx.conf