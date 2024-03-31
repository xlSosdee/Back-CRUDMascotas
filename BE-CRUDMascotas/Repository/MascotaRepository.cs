using BE_CRUDMascotas.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

//IMPLEMENTACION PATRON REPOSITORY
namespace BE_CRUDMascotas.Repository
{
    public class MascotaRepository: IMascotaRepository
    {   
        private readonly AplicationDbContext _context;
        public MascotaRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task DeleteMascota(int id)
        {
            var mascotaEliminar = await GetMascotaById(id);
            
            _context.Mascotas.Remove(mascotaEliminar);
            await _context.SaveChangesAsync();
        }

        public async Task EditMascota(Mascota mascota)
        {
            //Si hacemos SOLO lo de las lineas de abajo actualizaremos TODOS los campos existentes en la base de datos relativos al ID proporcionado
            //Si no le pasas un objeto con ID agregará dicho elemento como si fuera nuevo a la base de datos.
            //_context.Mascotas.Update(mascota);
            //await _context.SaveChangesAsync();

            //De esta otra manera actualizaremos solo los datos que indiquemos:

            var mascotaActualizar = await GetMascotaById(mascota.Id);
            
            mascotaActualizar.Nombre = mascota.Nombre;
            mascotaActualizar.Color = mascota.Color;
            mascotaActualizar.Raza = mascota.Raza;
            mascotaActualizar.Peso = mascota.Peso;
            mascotaActualizar.Edad = mascota.Edad;


            await _context.SaveChangesAsync();
        }

        public async Task<List<Mascota>> GetListMascotas()
        {
            //Para usar el ToListAsync debemos importar el EntityFrameworkCore
            return await _context.Mascotas.ToListAsync();
        }

        public async Task<Mascota> GetMascotaById(int id)
        {
            var mascotaBuscada = await _context.Mascotas.FindAsync(id);

            if (mascotaBuscada == null)
            {
                throw new InvalidOperationException($"No se encontró una mascota con el ID {id}");
            }

            return mascotaBuscada;
        }

        public async Task<Mascota> PostMascota(Mascota mascota)
        {
            mascota.FechaRegistro = DateTime.Now;
            _context.Mascotas.Add(mascota);
            await _context.SaveChangesAsync();

            return mascota;
        }
    }
}
