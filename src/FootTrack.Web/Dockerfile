FROM node AS compile-image
WORKDIR /app/front

COPY package.json ./
RUN npm install

COPY . .
ARG Environment="Debug"
RUN if [ "$Environment" = "Debug" ]; then npm run build; else npm run build --prod; fi

FROM nginx
WORKDIR /var/www/front
COPY --from=compile-image /app/front/dist/foot-track .
COPY nginx.conf /etc/nginx