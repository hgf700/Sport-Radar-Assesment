#front
FROM node:20 as build_front
WORKDIR /app
COPY . .
RUN npm install && npm run build

FROM nginx
COPY --from=build_front /app/dist/front/browser /usr/share/nginx/html