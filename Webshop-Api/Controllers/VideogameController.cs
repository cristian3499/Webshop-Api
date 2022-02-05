using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop_Api.Context;
using Webshop_Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Webshop_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideogameController : ControllerBase
    {
        //Mandamos a llamar al coontext
        private readonly AppDbContext context;
        //Creando contructor para el controlador
        public VideogameController(AppDbContext context)
        {
            this.context = context;
        }

        /*Vamos a crear los petodos del API*/
        /*Siempre cambiar el string del metodo por un ActionResult*/



        // GET: api/<VideogameController>
        [HttpGet]
        public ActionResult Get()
        {
            //Manejo de errores
            try
            {
                return Ok(context.Videogames.ToList());
            }
            catch (Exception ex)
            {
                //Si existe un error, lo regresamos al cliente
                return BadRequest(ex.Message);
            }
        }

        // GET api/<VideogameController>/5
        [HttpGet("{id}", Name = "GetVideogame")] /*Como lo vamos a reutilizar en algunos metodos, le ponemos un nombre*/
        public ActionResult Get(int id)
        {
            //Esta peticion es cuando buscamos algun registro en especifico
            try
            {
                var videojuego = context.Videogames.FirstOrDefault(f => f.id == id);
                return Ok(videojuego);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<VideogameController>
        [HttpPost]
        public ActionResult Post([FromBody] Videogame videogame)
        {
            try
            {
                context.Videogames.Add(videogame);//Insertar el registro
                context.SaveChanges();//Guardamos cambios
                return CreatedAtRoute("GetVideogame", new { id = videogame.id }, videogame);//Retornamos el objeto qu se inserto
                // Y para obtener el id que se acaba de insertar reutilizamos la funcion GetVideogame
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<VideogameController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Videogame videogame)
        {
            try
            {
                if (videogame.id == id)//Comparamos si el id coincide tanto el que viene por parametro como el del body
                {
                    context.Entry(videogame).State = EntityState.Modified;//Se hace la modificacioon
                    context.SaveChanges();//Guardamos cambios
                    return CreatedAtRoute("GetVideogame", new { id = videogame.id }, videogame);//Se regresa el objeto obtenido
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<VideogameController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var videogame = context.Videogames.FirstOrDefault(f => f.id == id);//Buscamos su elemento id de la tabla
                if (videogame != null)
                {
                    context.Videogames.Remove(videogame);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
