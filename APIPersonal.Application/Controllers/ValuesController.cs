using APIPersonal.Domain.Persona;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIPersonal.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    //[Produces("application/json")]
    //[Route("Persona")]
    public class PersonaaController : Controller
    {
        IPersona _ipersona;
        public PersonaaController(IPersona ipersona)
        {
            _ipersona = ipersona;
        }

        [HttpGet("{nombre}")]
        public IActionResult GetPersona(string nombre)
        {
            if (_ipersona.ValidaPersona(nombre))
            {
                var persona = _ipersona.GetPersona(nombre);
                return Ok(new
                {
                    state = "OK",
                    message = "Persona Encontrada",
                    result = persona
                });
            }
            else
            {
                return Ok(new
                {
                    state = "NOK",
                    message = "Persona No Encontrada"
                });
            }
        }

        //INSERTAR COMPROBANTE
        [HttpPost]
        [Route("Enviar")]
        public IActionResult PostPersona([FromBody] Persona persona)
        {

            if (!_ipersona.ValidaPersona(persona.Nombre))
            {
                if (_ipersona.InsertarPersona(persona))
                {
                       
                    return Ok(new
                    {
                        state = "OK",
                        message = "El Comprobante se registró correctamente",
                        Comprobante = persona.Nombre
                    });
                }
                else
                {
                    return Ok(new
                    {
                        state = "NOK",
                        message = "El Comprobante no se registró",
                        result = persona.Nombre
                    });
                };
            }
            else
            {
                return Ok(new
                {
                    state = "OK",
                    message = "El Comprobante ya se encuentra registrado.",
                    result = persona.Nombre
                });
            };
        }

    }
}
