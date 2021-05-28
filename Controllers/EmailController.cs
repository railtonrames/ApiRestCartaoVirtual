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