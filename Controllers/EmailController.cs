using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IARCV_InterfaceRepository _repo;

        public EmailController(IARCV_InterfaceRepository repo)
        {
            _repo = repo;
        }

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

        // GET: api/Email/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok();
        }

        // POST: api/Email
        [HttpPost]
        public ActionResult Post(Email model)
        {
            try
            {
                using var contexto = new EmailContext();
                contexto.Email.Add(model);
                contexto.SaveChanges();

                return Ok("Inserido com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT: api/Email/5
        [HttpPut("{id}")]
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}