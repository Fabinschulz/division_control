# Define a imagem base
FROM node:15

# Define o diretório de trabalho do contêiner
WORKDIR /app

# Copia o código do projeto para o contêiner
COPY . .

# Instala as dependências
RUN npm install --force

# Compila o projeto
RUN npm run

# Expõe a porta 3000
EXPOSE 3080

# Define o comando para iniciar o servidor HTTP do Angular
CMD ["npm", "run"]
