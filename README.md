[![.NET](https://github.com/jonataspc/MyBlog/actions/workflows/dotnet.yml/badge.svg)](https://github.com/jonataspc/MyBlog/actions/workflows/dotnet.yml)

# **MyBlog - Aplicação de Blog Simples com MVC e API RESTful**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **MyBlog**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Introdução ao Desenvolvimento ASP.NET Core**.
O objetivo principal desenvolver uma aplicação de blog que permite aos usuários criar, editar, visualizar e excluir posts e comentários, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful.
Descreva livremente mais detalhes do seu projeto aqui.

### **Autor(es)**
- Jonatas Cruz


## **2. Proposta do Projeto**

O projeto consiste em:

- **Aplicação MVC:** Interface web para interação com o blog.
- **API RESTful:** Exposição dos recursos do blog para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e usuários comuns.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:** SQL Server ou SQLite
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estilização básica
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:

- src/
  - MyBlog.Core: camada central contendo as entidades, modelos de domínio da aplicação, interfaces, regras de negócio, persistência de dados e injeção de dependências.
  - MyBlog.Web.Mvc: interface web da aplicação, utilizando o padrão MVC.
  - MyBlog.Web.Api: API RESTful da aplicação.
	
- tests/MyBlog.Tests.Integration: testes de integração da Web API.
	
- README.md: Arquivo de Documentação do Projeto
- FEEDBACK.md: Arquivo para Consolidação dos Feedbacks
- .gitignore: Arquivo de Ignoração do Git
- .gitattributes: Atributos do Git
- .editorconfig: Preferências de Estilo de Código

## **5. Funcionalidades Implementadas**

- **CRUD para Posts e Comentários:** Permite criar, editar, visualizar e excluir posts e comentários.
- **Autenticação e Autorização:** Diferenciação entre usuários comuns e administradores.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.
- Template utilizada no projeto MVC: https://startbootstrap.com/template/blog-home  
- Uso da lib [Timeago](https://timeago.yarp.com/) para apresentação de data/hora no front-end. 
- Contador de views nos posts.
- Feature de pesquisa de posts por palavra-chave. 
- Paginação de posts na home e também na WebAPI.


## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/jonataspc/MyBlog.git`
   - `cd MyBlog`

2. **Configuração do Banco de Dados:**
   - Para execução em ambiente de desenvolvimento será utilizado SQLite (string de conexão consta em `appsettings.Development.json`). Para ambiente de produção será utilizado SQL Server (string de conexão consta em `appsettings.Production.json`)
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos


3. **Executar a Aplicação MVC:**
   - `cd src\MyBlog.Web.Mvc`
   - `dotnet run --launch-profile "https"`
   - Acesse a aplicação em: https://localhost:7160

4. **Executar a API:**
   - `cd src\MyBlog.Web.Api`
   - `dotnet run --launch-profile "https"`
   - Acesse a documentação da API em: https://localhost:7161/swagger

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `\src\MyBlog.Web.Api\appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar manualmente devido a configuração do seed de dados. Os dados de teste (usuários, autores, posts e comentários) são populados através da lib [Bogus](https://github.com/bchavez/Bogus).
- **Credenciais do usuário admin criado por padrão:** E-mail: `admin@admin.com`, senha: `Admin123!`

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em https://localhost:7161/swagger

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
