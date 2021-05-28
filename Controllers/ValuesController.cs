using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRestCartaoVirtual.Data;
using ApiRestCartaoVirtual.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestCartaoVirtual.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        // GET * api/values
        [HttpGet]
        public ActionResult Get()
        {
            using var contexto = new EmailContext();
            var listEmail = contexto.Email.ToList();
            return Ok(listEmail);
        }

        // GET FILTRO api/values/filtro/<E-MAIL>
        [HttpGet("filtro/{endereco}")]
        public ActionResult GetFiltro(String endereco)
        {
            using var contexto = new EmailContext();
            var listEmail = contexto.Email.Where(x => x.Endereco.Contains(endereco)).ToList();
            return Ok(listEmail);
        }

        //INSERT api/values/<E-MAIL>
        [HttpGet("{enderecoEmail}")]
        public ActionResult Get(String enderecoEmail)
        {
            StringBuilder sb = new StringBuilder(16);
            Random random = new Random();
            for (int i = 0; i < 16; i++){sb.Append(random.Next(0, 9));}

            var email = new Email
            {
                Endereco = enderecoEmail,
                Cartoes = new List<Cartao>
                                    {
                                        new Cartao { Numero = sb.ToString()}
                                    }
            };
            using (var contexto = new EmailContext())
            {

                contexto.Email.Add(email);
                contexto.SaveChanges();
            }
            return Ok();
        }

        //INSERT api/values/update/<E-MAIL NOVO>/<ID>
        //[HttpGet("update/{Id}")]
        //public ActionResult Update(int id)
        //{
        //    using (var contexto = new EmailContext())
        //    {
        //        var email = contexto.Email.Where(x => x.Id == "3").Single();
        //        email.Endereco = "teste3";
        //        contexto.SaveChanges();
        //    }
        //    return Ok();

        //}


        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/delete/<ID>
        [HttpGet("delete/{id}")]
        public void Delete(int id)
        {
            using var contexto = new EmailContext();
            var email = contexto.Email.Where(x => x.Id == id).Single();
            contexto.Email.Remove(email);
            contexto.SaveChanges();
        }
    }
}