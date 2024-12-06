# Aiko Learning - Backend

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/logo.png" alt="logo" />
</div>

## 💻 Sobre o projeto
O **Aiko Learning** é um gestor de artigos criado com o propósito de experimentar novas tecnologias e conceitos da a empresa **Aiko**. Este projeto serve para implementar provas de conceito (PoCs) que possam validar melhorias e novas funcionalidades para o projeto principal da empresa.

## 🛠️ Tecnologias Utilizadas
- **Backend: Linguagens e Frameworks**
  - C#
  - .NET

- **Banco de Dados**
  - PostgreSQL
  - MongoDB

- **ORM e Data Access**
  - Entity Framework
  - Dapper

- **Testes  Automatizados**
  - xUnit
  - Moq
  - Fluent Assertions

- **Comunicação em Tempo Real**
  - SignalR

- **Frontend: Linguagens e Frameworks**
  - Html
  - Css
  - Javascript
  - Vue.js
  - BootstrapVue

- **Logs e Monitoramento**
  - Prometheus
  - Grafana

## ⚙️ Funcionalidades
- **Autenticação e Autorização**
  - [x] Registro de novos usuários
  - [x] Login
  - [x] Logout

- **Gestão de Conteúdo**
  - [x] Cadastro e filtragem de categorias e artigos
  - [x] Gerenciamento de usuários e permissões

- **Sincronização**
  - [x] Sincronização de estatísticas

- **Suporte**
  - [x] Chat em tempo real para suporte

## 📦 Padrões de Design Implementados
- [x] SOLID
- [x] APIs REST
- [x] Clean Architecture
- [x] CQRS
  - [x] Leitura: Dapper (MongoDB)
  - [x] Escrita: Entity Framework (PostgreSQL)
- [x] Repository Pattern
- [x] Unit of Work
- [x] Auditoria de Dados
- [x] Global Error Handler
- [x] Paginação de APIs
- [x] Testes Unitários
- [ ] Testes de Integração
- [ ] Testes e2e
- [ ] Logs e Monitoramento
- [ ] CI/CD Pipelines

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
   $ dotnet restore
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

# O servidor inciará na porta:5066 - acesse http://localhost:5066/swagger/index.html

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
$ npm run serve

# A aplicação será aberta na porta:8080 - acesse http://localhost:8080

```

#### ✅ Executando os Testes do Backend
```bash
 $  dotnet test
```
**VS Code**
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/vs-code-tests.png" alt="logo" />
</div>

**Terminal**
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/terminal-tests.png" alt="logo" />
</div>

## 🎨 Layout 

 #### Login

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/login-dark.png" alt="logo" />
</div>

 #### Registrar

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/register-dark.png" alt="registrar usuário" />
</div>

 #### Home

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/home-dark.png" alt="home do sistema" />
</div>

 #### Listagem de Artigos

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/articles-dark.png" alt="listagem de artigos" />
</div>

 #### Gestão de Artigos

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/admin-articles-dark.png" alt="gestão de artigos" />
</div>

 #### Swagger: Rotas do Backend

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/swagger.png" alt="rotas do backend" />
</div>

## 📝 Licença

Este projeto esta sobe a licença [MIT](https://github.com/Willian-Brito/aiko-learning-client/blob/main/LICENSE).

Feito com ❤️ por Willian Brito 👋🏽 [Entre em contato!](https://www.linkedin.com/in/willian-ferreira-brito/)