# API REST de Geração de Cartão de Crédito Virtual

# Sobre o projeto

Consiste em uma API REST que permite o cadastro de e-mails e geração aleatória de cartões de crédito virtual, feito em C# com .Net Core e Entity Framework Core com conexão à banco de dados SQL Server, contruído durante o **Teste técnico do Projeto Vaivoa**, é a terceira etapa do processo seletivo organizado pela [Vaivoa](https://vaivoa.com "Site do Projeto").

A API baseia-se em cadastro de e-mails e gerações de cartões, onde há endpoints que permitem pesquisar, inserir, modificar e excluir e-mails e cartões.

Abaixo na seção "Passo-a-passo" você poderá ler o artigo descrevendo os passos para a criação dessa API:

# 🛠 Tecnologias utilizadas
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCartaoVirtual.Models
{
    public class Cartao
    {
        public int Id { get; set; }
        public String Numero { get; set; }
        public Email Email { get; set; }
        public int EmailId { get; set; }
    }
}
```
- Passo 8: Abra o data/EmailContext.cs e crie os Dbsets do contexto. Confira o código abaixo:
```c#
using ApiRestCartaoVirtual.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCartaoVirtual.Data
{
    public class EmailContext : DbContext
    {
        public DbSet<Email> Email { get; set; }
        public DbSet<Cartao> Cartao { get; set; }
    }
}
```
- Passo 9: Identifique a sua String de conexão e crie-a.
![Instalando Pacotes](?)
- Passo 10: Adicione a sua String de conexão com o banco de dados no EmailContext.cs. Confira o código abaixo:
```c#
using ApiRestCartaoVirtual.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCartaoVirtual.Data
{
    public class EmailContext : DbContext
    {
        public DbSet<Email> Email { get; set; }
        public DbSet<Cartao> Cartao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Password=sa123456;Persist Security Info=True;User ID=sa;Initial Catalog=CartaoVirtualApp;Data Source=DESKTOP-UQDBKFD");
        }
    }
}
```
- Passo 11: Abra o package manager console (View > Other Windows > Package Manager Console), crie a migration (Add-Migration) e crie o banco de dados (Update-Database). Confira abaixo:

Criando a migration:
```bash
Add-Migration Initial
```
Executando a migration e criando o banco de dados Sql Server:
```bash
Update-Database
```
- Passo 12: Crie a classe "EmailController.cs" no diretório Controllers e insira os actions results/endpoints.
```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRestCartaoVirtual.Data;
using ApiRestCartaoVirtual.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRestCartaoVirtual.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        // GET: api/Email
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(new Email());
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET: api/Email/listar/{endereco}
        [HttpGet("listar/{endereco}")]
        public ActionResult Get(String endereco)
        {
            using var contexto = new EmailContext();
            var listEmail = contexto.Email.Where(x => x.Endereco.Contains(endereco))
                .Include("Cartoes")
                .ToList();
            return Ok(listEmail);
        }

        // POST: api/Email/inserir/{endereco}
        [HttpPost("inserir/{endereco}")]
        public ActionResult Post(String endereco)
        {
            try
            {
                StringBuilder sb = new StringBuilder(16);
                Random random = new Random();
                for (int i = 0; i < 16; i++) { sb.Append(random.Next(0, 9)); }

                var email = new Email{
                    Endereco = endereco,
                    Cartoes = new List<Cartao>
                        {
                            new Cartao { Numero = sb.ToString() }
                        }
                    };

                using var contexto = new EmailContext();
                contexto.Email.Add(email);
                contexto.SaveChanges();

                return Ok("Inserido com sucesso !\n Cartão Nº:" + sb.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT: api/Email/novocartao/{endereco}
        [HttpPut("novocartao/{endereco}")]
        public ActionResult GerarCartao(String endereco)
        {
            try
            {
                StringBuilder sb = new StringBuilder(16);
                Random random = new Random();
                for (int i = 0; i < 16; i++) { sb.Append(random.Next(0, 9)); }

                using var contexto = new EmailContext();

                var Email = (contexto.Email.AsNoTracking().FirstOrDefault(e => e.Endereco == endereco));

                var novoCartao = new Cartao {
                    EmailId = Email.Id,
                    Numero = sb.ToString() 
                };               

                if (Email != null)
                {
                    contexto.Add(novoCartao);
                    contexto.SaveChanges();

                    return Ok("Gerado com sucesso !\n Cartão Nº:" + sb.ToString());
                }
                return Ok("Não encontrado !");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT: api/Email/alterar/{id}
        [HttpPut("alterar/{id}")]
        public ActionResult Put(int id, Email model)
        {
            try
            {
                using var contexto = new EmailContext();

                if(contexto.Email.AsNoTracking().FirstOrDefault(e => e.Id == id) != null)
                {
                    contexto.Update(model);
                    contexto.SaveChanges();

                    return Ok("Alterado com sucesso !");
                }
                return Ok("Não encontrado !");
                     
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // DELETE: api/delete/{id}
        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                using var contexto = new EmailContext();
                if (contexto.Email.AsNoTracking().FirstOrDefault(e => e.Id == id) != null)
                {       
                    var email = contexto.Email.Where(x => x.Id == id).Single();
                    contexto.Email.Remove(email);
                    contexto.SaveChanges();
                    return Ok("Excluído com sucesso !");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não encontrado !");
        }
    }
}
```
- Passo 13: No arquivo "Startup.cs", chame a biblioteca "NewtonsoftJson" dentro do método "ConfigureServices".
```c#
services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
```
- Passo 14: Abra o arquivo de configuração "launchSettings.json" dentro do diretório Properties e altere a launchUrl de "ApiRestCartaoVirtual" e "profiles". Veja o exemplo abaixo:
```json
"launchUrl": "api/email"
```
- Passo 15: Testar e utilizar a API. Endpoints:
  - GET: api/Email  -> Retorna um modelo do objeto e-mail vazio.
  ![GET: api/Email](?)
  - GET: api/Email/listar/{endereco} -> Lista todos os cartões de crédito virtuais de um solicitante, conforme o e-mail passado no parâmetro.
  ![GET: api/Email/listar/{endereco}](?)
  - POST: api/Email/inserir/{endereco} -> Insere o e-mail passado no parâmetro e retorna um número aleatório de cartão de crédito.
  ![POST: api/Email/inserir/{endereco}](?)
  - PUT: api/Email/novocartao/{endereco} -> Insere um novo cartão de crédito aleatório no e-mail passado como parâmetro.
  ![PUT: api/Email/novocartao/{endereco}](?)
  - PUT: api/Email/alterar/{id} -> Altera o registro do ID passado no parâmetro conforme o model json que for enviado.
  ![PUT: api/Email/alterar/{id}](?)
  - DELETE: api/delete/{id} -> Deleta um registro conforme a ID passada no parâmetro.
  ![DELETE: api/delete/{id}](?)

# Autor

Railton Rames Sousa

https://www.linkedin.com/in/railton-rames/
