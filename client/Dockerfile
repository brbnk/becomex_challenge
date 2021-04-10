FROM mhart/alpine-node:15

WORKDIR /app

COPY package.json .

RUN npm i npm@latest -g
RUN npm install -g @angular/cli

RUN npm install

COPY . .

EXPOSE 4200

ENTRYPOINT [ "npm", "start"]