# API REST de Geração de Cartão de Crédito Virtual

# Sobre o projeto

Consiste em uma API REST que permite o cadastro de e-mails e geração aleatória de cartões de crédito virtual, feito em C# com .Net Core e Entity Framework Core com conexão à banco de dados SQL Server, contruído durante o **Teste técnico do Projeto Vaivoa**, é a terceira etapa do processo seletivo organizado pela [Vaivoa](https://vaivoa.com "Site do Projeto").

A API baseia-se em cadastro de e-mails e gerações de cartões, onde há endpoints que permitem pesquisar, inserir, modificar e excluir e-mails e cartões.

Abaixo na seção "Passo-a-passo" você poderá ler o artigo descrevendo os passos para a criação dessa API:

# Tecnologias utilizadas
## Ambientes de desenvolvimento
- Visual Studio 2019
- Microsoft Sql Server Manegement Studio 18
- Postman
## Bibliotecas NuGet
- Microsoft.AspNetCore.Mvc.NewtonsoftJson
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Analyzers
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Relational
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
## Implantação
- Front/Back end: C# com .Net Core e Entity Framework Core
- Banco de dados: SQL Server

## Diagrama da Banco de Dados Sql Server
![Diagrama BD](https://github.com/railtonrames/ApiRestCartaoVirtual/blob/master/Assets/Diagrama_CartaoVirtualApp.PNG)

# Passo-a-passo
Pré-requisitos: Visual Studio 2019, SQL Server 2019 Express e bibliotecas NuGet informadas acima.

- Passo 1: Após a instalação dos pré-requisitos, abra o Visual Studio 2019 e crie um novo projeto do tipo ASP.NET Core Web Application.
![Criando_Projeto](?)
- Passo 2: Crie os diretórios models e data.
![Criando_Diretórios](?)
- Passo 3: Crie as classes "cartao.cs" e "email.cs" dentro do diretório models.
![Criando Classes 1](?)
- Passo 4: Crie a classe de contexto chamada de "EmailContext" dentro do diretório data.
![Criando Classes 2](?)
- Passo 5: Instale os pacotes NuGets.
![Instalando Pacotes](?)
- Passo 6: Abra o models/email.cs e crie as propriedades da classe. Conforme o código abaixo:
```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCartaoVirtual.Models
{
    public class Email
    {
        public int Id { get; set; }
        public String Endereco { get; set; }
        public List<Cartao> Cartoes { get; set; }
    }
}
```
- Passo 7: Abra o models/cartao.cs e crie as propriedades da classe. Conforme o código abaixo:
```c#
?
```
- Passo 8: Abra o data/EmailContext.cs e crie os Dbsets do contexto. Confira o código abaixo:
```c#
?
```
- Passo 9: Identifique a sua String de conexão e crie-a.
![Instalando Pacotes](?)
- Passo 10: Adicione a sua String de conexão com o banco de dados no EmailContext.cs. Confira o código abaixo:
```c#
?
```
- Passo 11: Abra o package manager console (View > Other Windows > Package Manager Console), crie a migration (Add-Migration) e crie o banco de dados (Update-Database). Confira abaixo:
  - Criando a migration:
  ```bash
  Add-Migration Initial
  ```
  - Executando a migration e criando o banco de dados Sql Server:
  ```bash
  Update-Database
  ```
- Passo 12: Crie a classe "EmailController.cs" no diretório Controllers e insira os actions results/endpoints.
- Passo 13: No arquivo "Startup.cs", chame a biblioteca "NewtonsoftJson" dentro do método "ConfigureServices".
- Passo 14: Abra o arquivo de configuração "launchSettings.json" dentro do diretório Properties e altere a launchUrl de "ApiRestCartaoVirtual" e "profiles".
- Passo 15: Testar e utilizar a API. Endpoints:
  - GET: api/Email  -> Retorna um modelo do objeto e-mail vazio.
  - POST: api/Email/inserir/{endereco} -> Insere o e-mail passado no parâmetro e retorna um número aleatório de cartão de crédito.
  - PUT: api/Email/novocartao/{endereco} -> Insere um novo cartão de crédito aleatório no e-mail passado como parâmetro.
  - PUT: api/Email/alterar/{id} -> Altera o registro do ID passado no parâmetro conforme o model json que for enviado.
  - DELETE: api/delete/{id} -> Deleta um registro conforme a ID passada no parâmetro.

# Autor

Railton Rames Sousa

https://www.linkedin.com/in/railton-rames/
