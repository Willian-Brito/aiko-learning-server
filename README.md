# Aiko Learning - Backend

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/logo.png" alt="logo" />
</div>

## 💻 Sobre o projeto
O **Aiko Learning** é um gestor de artigos que desenvolvi como uma iniciativa pessoal para experimentar novas tecnologias e conceitos que possam trazer melhorias para os projetos da **Aiko** e me ajudar a crescer como profissional dentro da empresa.

Esse projeto foi criado como uma plataforma para **desenvolver** e **testar minhas próprias ideias** e conceitos inovadores. Ele serve para implementar **provas de conceito (PoCs)**, validando essas ideias antes de propô-las para aplicação no projeto principal da empresa. 

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

- **Sincronização de Estatísticas**
  - FluentScheduler

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

- **Chat**
  - [x] Chat em tempo real

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
- [x] Jobs para Sincronização de Estatísticas
- [x] Paginação de APIs
- [x] Testes Unitários
- [ ] Testes de Integração
- [ ] Testes e2e
- [x] Rate Limiting
- [x] Conteinerização
- [ ] Logs e Monitoramento
- [ ] CI/CD Pipelines
- [ ] Análise de Vulnerabilidades (SAST/DAST/SCA)

## 🔧 Instalação
>Se você deseja executar o projeto localmente sem o uso de containers, siga as instruções abaixo.
Caso prefira utilizar Docker, vá diretamente para a seção "Como executar o projeto".

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

> **Requisito:** É necessário ter o docker instalado em seu sistema operacional (Linux, Windows ou Mac)

Este projeto é divido em duas partes:
1. Backend ([Server](https://github.com/Willian-Brito/aiko-learning-server)) 
2. Frontend ([Client](https://github.com/Willian-Brito/aiko-learning-client))

#### 🎲 Rodando o Backend

```bash

# Clone este repositório
$ git clone https://github.com/Willian-Brito/aiko-learning-server

# Vá para a pasta server
$ cd aiko-learning-server

# Execute o docker compose
$ docker-compose up --build

# O servidor inciará na porta:5066 - acesse http://localhost:5066/swagger/index.html

```

> Certifique-se de que as portas **5432 (PostgreSQL)** e **27017 (MongoDB)** não estejam sendo utilizadas por outros serviços em sua máquina local.

#### 🧭 Rodando o Frontend

```bash

# Clone este repositório
$ git clone https://github.com/Willian-Brito/aiko-learning-client

# Vá para a pasta da aplicação Front End
$ cd aiko-learning-client

# Execute o docker compose
$ docker-compose up --build

# A aplicação será aberta na porta:8080 - acesse http://localhost:8080

```

#### ✅ Executando os Testes do Backend
```bash
 $  dotnet test
```
**VS Code**
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/vs-code-tests.png" alt="vs code" />
</div>

**Terminal**
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/terminal-tests.png" alt="terminal" />
</div>

## 🎨 Layout 

#### Demo
<div align="center">
  <img src="https://github.com/Willian-Brito/aiko-learning-client/blob/main/src/assets/gif/demo.gif?raw=true" alt="demo" />
</div>

#### Login

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/login-dark.png" alt="login" />
</div>

#### Registrar

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/register-dark.png" alt="registrar usuário" />
</div>

#### Home

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/home-dark.png" alt="home do sistema" />
</div>

#### Perfil

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/profile-dark.png" alt="listagem de artigos" />
</div>

#### Listagem de Artigos

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/articles-dark.png" alt="listagem de artigos" />
</div>

#### Gestão de Artigos

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/admin-articles-dark.png" alt="gestão de artigos" />
</div>

#### Chat
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/chat-dark.png" alt="chat em tempo real" />
</div>

#### Swagger: Rotas do Backend

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/aiko-learning-client/refs/heads/main/src/assets/prints/swagger.png" alt="rotas do backend" />
</div>

## 📝 Licença

Este projeto esta sobe a licença [MIT](https://github.com/Willian-Brito/aiko-learning-server/blob/main/LICENSE).

Feito com ❤️ por Willian Brito 👋🏽 [Entre em contato!](https://www.linkedin.com/in/willian-ferreira-brito/)