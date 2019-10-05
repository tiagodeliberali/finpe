# build environment
FROM node:10.16.3-alpine as build
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY Finpe.React/package.json /app/package.json
RUN npm install --silent
RUN npm install react-scripts@3.0.1 -g --silent
COPY Finpe.React/. /app
ARG BRANCH
ARG COMMIT
RUN npm run build -- --env.COMMIT=${COMMIT} --env.BRANCH=${BRANCH}

# production environment
FROM nginx:1.16.0-alpine AS runtime
COPY --from=build /app/dist /var/www
COPY docker/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]