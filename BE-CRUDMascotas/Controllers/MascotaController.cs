using Azure.Core;
using BE_CRUDMascotas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO.Compression;
using System.Text.Json.Serialization;
using System.Text.Json;
using BE_CRUDMascotas.Models.DTOs;
using AutoMapper;
using Microsoft.OpenApi.Validations;
using BE_CRUDMascotas.Repository;

namespace BE_CRUDMascotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly IMascotaRepository _mascotaRepository;

        public MascotaController(IMascotaRepository mascotaRepository, IMapper mapper)
        {
            _mascotaRepository = mascotaRepository;
            _mapper = mapper;
        }

        //ENDPOINTS DEL PROYECTO
        [HttpGet]

        public async Task<IActionResult> Get() {
            try
            {

                var listMascotas = await _mascotaRepository.GetListMascotas();
                //En el _mapper podemos colocar: <Tipo de dato origen, Tipo de dato a mapear>
                //var listMascotasDTO = _mapper.Map<List<Mascota>, List<MascotaDTO>>(listMascotas);
                //O más prolijo colocar de esta manera:

                var listMascotasDTO = _mapper.Map<IEnumerable<MascotaDTO>>(listMascotas);
                return Ok(listMascotasDTO);

            }
            catch (Exception ex) {
                return BadRequest(ex.Message);

            }


        }


        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            try
            {

                var verMascota = await _mascotaRepository.GetMascotaById(id);
                
                var verMascotaDTO = _mapper.Map<MascotaDTO>(verMascota);

                return Ok(verMascotaDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _mascotaRepository.DeleteMascota(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(MascotaDTO mascotaDto)
        {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDto);

                var postedMascota = await _mascotaRepository.PostMascota(mascota);

                var postedMascotaDto = _mapper.Map<MascotaDTO>(postedMascota);

                return CreatedAtAction("Get", new { id = postedMascotaDto.Id }, postedMascotaDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]

        public async Task<IActionResult> Put(MascotaDTO mascotaDto)
        {
            try
            {

                var mascota = _mapper.Map<Mascota>(mascotaDto);

                await _mascotaRepository.EditMascota(mascota);

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
