using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PracticaFinal1.Models;

namespace PracticaFinal1.Controllers
{
    [RoutePrefix("api")]
    public class SexoesController : ApiController
    {

        private DataContext db = new DataContext();

        // GET: api/Sexoes
        public IQueryable<Sexo> GetSexoes()
        {
            return db.Sexoes;
        }

        [HttpGet]
        [Route("{numero:int}")]
        public string Operacion(int numero)
        {
            if (numero < 0)
                return "ERROR";
            if (numero == 0)
                return "Realizado por Gean Valverde";
            return "Usted ingreso el numero " + numero.ToString();
        }
        // GET: api/Sexoes/5
        [ResponseType(typeof(Sexo))]
        public IHttpActionResult GetSexo(int id)
        {
            Sexo sexo = db.Sexoes.Find(id);
            if (sexo == null)
            {
                return NotFound();
            }

            return Ok(sexo);
        }

        // PUT: api/Sexoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSexo(int id, Sexo sexo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sexo.numero)
            {
                return BadRequest();
            }

            db.Entry(sexo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SexoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Sexoes
        [ResponseType(typeof(Sexo))]
        public IHttpActionResult PostSexo(Sexo sexo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sexoes.Add(sexo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sexo.numero }, sexo);
        }

        // DELETE: api/Sexoes/5
        [ResponseType(typeof(Sexo))]
        public IHttpActionResult DeleteSexo(int id)
        {
            Sexo sexo = db.Sexoes.Find(id);
            if (sexo == null)
            {
                return NotFound();
            }

            db.Sexoes.Remove(sexo);
            db.SaveChanges();

            return Ok(sexo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SexoExists(int id)
        {
            return db.Sexoes.Count(e => e.numero == id) > 0;
        }
    }
}