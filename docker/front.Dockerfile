# build environment
FROM node:10.16.3-alpine as build
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY Finpe.React/package.json /app/package.json
RUN npm install --silent
RUN npm install react-scripts@3.0.1 -g --silent
COPY Finpe.React/. /app
RUN npm run build

# production environment
FROM nginx:1.16.0-alpine AS runtime
COPY --from=build /app/build /var/www
COPY docker/nginx.conf /etc/nginx/nginx.conf
ARG API_URL
ENV API_BASE_URL $API_URL
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]