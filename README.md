# S4E Clientes - Gerenciamento de Clientes

Este repositório contém uma aplicação web desenvolvida em **ASP.NET MVC 5 (VB.NET)** para o gerenciamento de clientes. O projeto utiliza uma arquitetura em camadas focada em performance e manutenibilidade, implementando padrões modernos como **Async/Await**, **Dapper** para acesso a dados e uma interface rica com **Bootstrap 5** e **jQuery**.

## 🚀 Tecnologias Utilizadas

### Backend

* **Linguagem:** VB.NET
* **Framework:** ASP.NET MVC 5 (.NET Framework 4.7.2+)
* **Acesso a Dados:** Dapper
* **Banco de Dados:** SQL Server
* **Padrões:** DDD, Async/Await

### Frontend

* **Layout:** Bootstrap 5
* **Scripting:** jQuery

---

## ⚙️ Pré-requisitos

Antes de começar, certifique-se de ter instalado em sua máquina:

* [Visual Studio 2019 ou 2022](https://visualstudio.microsoft.com/) (com a carga de trabalho "ASP.NET e desenvolvimento web").
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express ou Developer) e [SSMS](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).
* .NET Framework 4.7.2 ou superior.

---

## 🛠️ Configuração e Instalação

### 1. Banco de Dados

Execute o script enviado por e-mail no seu SQL Server para criar o banco de dados e a tabela necessária.


### 2. Configuração da Aplicação

1. Clone este repositório:
```bash
git clone https://github.com/pj-lopes/S4E-clientes.git

```


2. Abra o arquivo de solução (`.sln`) no Visual Studio.
3. Abra o arquivo `Web.config` (na raiz do projeto Web).
4. Localize a seção `<connectionStrings>` e ajuste a string de conexão conforme seu ambiente SQL Server:

```xml
<connectionStrings>
    <add name="ConnectionSQLServer" 
         connectionString="Data Source=NOME-COMPUTADOR;Initial Catalog=s4e-clientes;Trusted_Connection=True;Connect Timeout=90;" 
         providerName="System.Data.SqlClient" />
</connectionStrings>

```

### 3. Dependências

O projeto utiliza o **NuGet** para gerenciar pacotes. Ao compilar o projeto pela primeira vez (`Ctrl + Shift + B`), o Visual Studio deve restaurar os pacotes automaticamente. Caso contrário, execute no Package Manager Console:

```powershell
Update-Package -Reinstall

```

---

## 📂 Estrutura do Projeto

A solução está organizada para separar responsabilidades:

* **S4E.Web (Presentation):** Contém os Controllers, Views e Scripts (JS/CSS).
* `Controllers/ClientesController.vb`: Gerencia as requisições HTTP.
* `Views/Clientes/Index.vbhtml`: Página inicial do projeto.
* `Scripts/site/site.js`: Lógica de frontend (Tabulator, Modal, AJAX).


* **S4E.Application:** Camada de serviço que orquestra as regras de negócio.
* `Services/ClientesServices.vb`: Validações de domínio antes de chamar o banco.


* **S4E.Domain:** Contém as entidades, enums e utilidades.
* `Entities/Cliente.vb`: Modelo de dados.
* `Utilidades/Validacoes.vb`: Entidade com métodos de validações.

* **S4E.Data:** Camada de acesso a dados.
* `DataAccess/ClientesDataAccess.vb`: Implementação do Dapper e comandos SQL.

---

## ✅ Funcionalidades Principais

1. **Listagem de Clientes:**
* Grid responsivo com paginação local (Tabulator).
* Filtro global por Nome ou Documento.
* Badges visuais para Pessoa Física/Jurídica.


2. **Cadastro:**
* Modal Bootstrap com validação em tempo real.
* Máscara dinâmica que alterna entre CPF e CNPJ conforme a digitação.
* Integração com Select2 para seleção de tipo de cliente.


3. **Segurança e Validação:**
* Validação de CPF/CNPJ (algoritmo matemático) no Backend.
* Validação de cadastro único por documento no Backend.
* Limpeza de dados (remoção de formatação) antes da persistência.


---

## 🖥️ Preview Visual

O sistema conta com um layout limpo.

* **Grid de Listagem:** Exibe dados formatados e feedback visual.
<img width="1357" height="403" alt="image" src="https://github.com/user-attachments/assets/984a7659-9f37-40a0-90ba-fc571f51fad6" />

* **Modal de Cadastro:** Formulário com validações visuais e mensagens de erro claras via Toastr.
<img width="608" height="623" alt="image" src="https://github.com/user-attachments/assets/5914821b-f1dc-492f-8b51-0c671aa3cf51" />


---

**Desenvolvido por Paulo Lopes**
