### STAGE 1: Build ###
ARG base_url

FROM node:16.13.2 AS build
WORKDIR /app
COPY package.json package-lock.json /app
RUN npm install
COPY . /app
RUN npm run build

### STAGE 2: Serve ###
FROM nginx:latest
COPY --from=build /app/dist/runesharper-client /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80