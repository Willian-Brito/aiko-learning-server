# Aiko Learning

## 💻 Sobre o projeto
O **Aiko Learning** é um gestor de artigos criado com o propósito de experimentar novas tecnologias e conceitos da a empresa **Aiko**. Este projeto serve para implementar provas de conceito (PoCs) que possam validar melhorias e novas funcionalidades para o projeto principal da empresa.

## 🛠️ Tecnologias Utilizadas
- **Linguagens e Frameworks**
  - C#
  - .NET

- **Banco de Dados**
  - PostgreSQL
  - MongoDB

- **ORM e Data Access**
  - Entity Framework
  - Dapper

- **Comunicação em Tempo Real e Monitoramento**
  - SignalR
  - Prometheus
  - Grafana

## ⚙️ Funcionalidades
- **Autenticação e Autorização**
  - [x] Registro de novos usuários
  - [x] Login

- **Gestão de Conteúdo**
  - [x] Cadastro e filtragem de categorias e artigos
  - [x] Gerenciamento de usuários e permissões

- **Sincronização**
  - [x] Sincronização de estatísticas

- **Suporte**
  - [ ] Chat em tempo real para suporte

## 📦 Padrões de Design Implementados
- **SOLID**
- **Clean Architecture**
- **CQRS**
  - Leitura: Dapper (MongoDB)
  - Escrita: Entity Framework (PostgreSQL)
- **Repository Pattern**
- **Unit of Work**
- **Auditoria de Dados**
- **Global Error Handler**
- **Paginação de APIs**
- **Logs e Monitoramento**
- **CI/CD Pipelines**

## 🔧 Instalação
1. **Pré-requisitos**: 
   - Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
    [Git](https://git-scm.com), [Node.js](https://nodejs.org/en/), [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0), [PostgreSQL](https://www.postgresql.org/download/) e [MongoDB](https://www.mongodb.com/try/download/community). 
    Além disto é bom ter um editor para trabalhar com o código como [VSCode](https://code.visualstudio.com/).

2. **Configuração do Banco de Dados**:
   - Configure o PostgreSQL e MongoDB, criando as bases de dados necessárias.
   - Atualize as strings de conexão no arquivo de configuração da aplicação.

3. **Instalando as Dependências**:
   ```bash
   dotnet restore
    ```

## 🚀 Como executar o projeto

Este projeto é divido em duas partes:
1. Backend ([Server](https://github.com/Willian-Brito/aiko-learning-server)) 
2. Frontend ([Client](https://github.com/Willian-Brito/aiko-learning-client))

#### 🎲 Rodando o Backend (servidor)

```bash

# Clone este repositório
$ git clone https://github.com/Willian-Brito/aiko-learning-server

# Vá para a pasta server
$ cd aiko-learning-server/Presentation/WebAPI 

# Execute a aplicação
$ dotnet run

# O servidor inciará na porta:5066 - acesse http://localhost:5066

```


#### 🧭 Rodando a aplicação web (Frontend)

```bash

# Clone este repositório
$ git clone https://github.com/Willian-Brito/aiko-learning-client

# Vá para a pasta da aplicação Front End
$ cd aiko-learning-client

# Instale as dependências
$ npm install

# Execute a aplicação em modo de desenvolvimento
$ npm run dev

# A aplicação será aberta na porta:8080 - acesse http://localhost:8080

```

#### ✅ Executando os Testes do Backend
```bash
   dotnet test
```

## 🎨 Layout 
- Swagger
- Telas



## 📝 Licença

Este projeto esta sobe a licença [MIT](./LICENSE).

Feito com ❤️ por Willian Brito 👋🏽 [Entre em contato!](https://www.linkedin.com/in/willian-ferreira-brito/)