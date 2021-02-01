using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Business;
using ProAgil.Domain;

namespace ProAgil.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        public readonly IProAgilBusiness _business;

        public EventosController(IProAgilBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _business.GetAllEventosAsync();

                if(results != null && results.Any())
                    return Ok(results);

                return NotFound("Registro não encontrado.");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _business.GetEventoAsyncById(id, true);

                if(result != null)
                    return Ok(result);

                return NotFound("Registro não encontrado.");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }
        }

        [HttpGet("getByTema{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var result = await _business.GetAllEventosAsyncByTema(tema, true);

                if(result != null)
                    return Ok(result);

                return NotFound("Registro não encontrado.");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _business.Add(model);

                if(await _business.SaveChangesAsync())
                    return Created($"api/eventos/{model.Id}", model);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }

            return BadRequest();
        }

        [HttpPut()]
        public async Task<IActionResult> Put(int eventoId, Evento model)
        {
            try
            {
                var evento = await _business.GetEventoAsyncById(eventoId);

                if(evento == null)
                    return NotFound("Registro não encontrado.");

                _business.Update(model);

                if(await _business.SaveChangesAsync())
                    return Created($"api/eventos/{model.Id}", model);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int eventoId)
        {
            try
            {
                var evento = await _business.GetEventoAsyncById(eventoId);

                if(evento == null)
                    return NotFound("Registro não encontrado.");

                _business.Delete(evento);

                if(await _business.SaveChangesAsync())
                    return Ok();

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }

            return BadRequest();
        }

    }
}