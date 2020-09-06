FROM node AS compile-image
WORKDIR /app/front

COPY package.json ./
RUN npm install

COPY . .
ARG Environment="Debug"
RUN if [ "$Environment" = "Debug" ]; then npm run build; else npm run build --prod; fi

FROM nginx
COPY --from=compile-image /app/front/dist/foot-track/ usr/share/nginx/html