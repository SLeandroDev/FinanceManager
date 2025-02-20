# Finance Manager

Finance Manager é um sistema de controle financeiro pessoal, desenvolvido para gerenciar e visualizar transações financeiras de forma prática e eficiente. O projeto utiliza boas práticas de desenvolvimento, como **Clean Architecture** e **DDD**, para garantir um código mais organizado e escalável.

## Tecnologias e Ferramentas Utilizadas

### 🔧 **Backend**
- **.NET Core**: Framework robusto para o desenvolvimento de APIs e aplicativos web.
- **C#**: Linguagem de programação usada para a implementação do backend e lógica de negócios.
- **JWT (JSON Web Tokens)**: Utilizado para autenticação de usuários, garantindo que as sessões sejam seguras e validadas sem a necessidade de armazenar informações sensíveis no servidor.
- **API REST**: Arquitetura usada para garantir que a comunicação entre o frontend e o backend seja simples e eficiente.
- **Clean Architecture**: Padrão arquitetural usado para organizar o código em camadas e promover a separação de responsabilidades. A aplicação foi estruturada de acordo com os princípios do Clean Architecture, com pastas separadas para `Domain`, `Application`, `Infrastructure` e `API`.
- **DDD (Domain-Driven Design)**: Prática usada para modelar o domínio do sistema e facilitar a evolução do código à medida que o projeto cresce.

### 🌐 **Frontend**
- **HTML**: Estruturação da página e conteúdo da aplicação.
- **CSS**: Estilos e design para deixar a aplicação visualmente atraente.
- **JavaScript**: Linguagem de programação utilizada para lógica e manipulação da interface do usuário, como interações com formulários e chamadas à API.
- **Bootstrap**: Framework CSS utilizado para criar uma interface limpa e responsiva, adaptada para diferentes dispositivos.

### 🗄️ **Banco de Dados**
- **SQL Server**: Banco de dados relacional utilizado para armazenar as informações do sistema, como dados de usuários, transações financeiras, e outras informações essenciais para o funcionamento da aplicação. A comunicação com o banco de dados é feita utilizando **Entity Framework**.

## Funcionalidades

- **Cadastro e Login de Usuários**: Através de autenticação via JWT, usuários podem se registrar e fazer login de forma segura.
- **Cadastro de Transações**: Usuários podem registrar transações financeiras com categorias, datas e valores.
- **Relatórios e Gráficos**: O sistema exibe gráficos e relatórios para facilitar o entendimento das finanças pessoais.
- **Interface Responsiva**: Utiliza o Bootstrap para garantir que a interface seja responsiva em diferentes tamanhos de tela (desktop, tablet e celular).

## Estrutura do Projeto

O projeto segue a arquitetura **Clean Architecture** e o padrão **DDD** para manter o código organizado e modular. As principais pastas do projeto são:

- **Domain**: Contém as entidades e regras de negócio (ex.: `Usuario`, `Transacao`).
- **Application**: Contém a lógica de aplicação, como os casos de uso (ex.: `CadastrarUsuario`, `RegistrarTransacao`).
- **Infrastructure**: Implementa a comunicação com o banco de dados (SQL Server) e outros serviços externos.
- **API**: A camada de interface, onde os controladores (controllers) estão localizados.

## Como Rodar o Projeto

### 1. Clone o Repositório

```bash
git clone [https://github.com/usuario/repo.git](https://github.com/SLeandroDev/FinanceManager.git)

