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
using FinalPorga.Models;

namespace FinalPorga.Controllers
{
    [RoutePrefix("apiweb")]
    public class OPsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/OPs
        public IQueryable<OP> GetOps()
        {
            return db.Ops;
        }

        //Funcion
        [HttpGet]
        [Route("{numero:int}")]
        public string Operacion(int numero)
        {
            if (numero < 0)
                return "ERROR";
            if (numero == 0)
                return "Realizado por Gean Valverde";
            return "Usted ingreso el numero: " + numero.ToString();
        }


        // GET: api/OPs/5
        [ResponseType(typeof(OP))]
        public IHttpActionResult GetOP(int id)
        {
            OP oP = db.Ops.Find(id);
            if (oP == null)
            {
                return NotFound();
            }

            return Ok(oP);
        }

        // PUT: api/OPs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOP(int id, OP oP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != oP.numero)
            {
                return BadRequest();
            }

            db.Entry(oP).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OPExists(id))
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

        // POST: api/OPs
        [ResponseType(typeof(OP))]
        public IHttpActionResult PostOP(OP oP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ops.Add(oP);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = oP.numero }, oP);
        }

        // DELETE: api/OPs/5
        [ResponseType(typeof(OP))]
        public IHttpActionResult DeleteOP(int id)
        {
            OP oP = db.Ops.Find(id);
            if (oP == null)
            {
                return NotFound();
            }

            db.Ops.Remove(oP);
            db.SaveChanges();

            return Ok(oP);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OPExists(int id)
        {
            return db.Ops.Count(e => e.numero == id) > 0;
        }
    }
}