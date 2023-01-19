### STAGE 1: Build ###
FROM node:16.13.2 AS build
WORKDIR /app
COPY package.json package-lock.json /app
RUN npm install
COPY . /app
RUN npm run build --prod

### STAGE 1: Serve ###
FROM nginx:latest
COPY --from=build /app/dist/runesharper-client /usr/share/nginx/html
EXPOSE 80