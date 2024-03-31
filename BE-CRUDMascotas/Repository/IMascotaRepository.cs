using BE_CRUDMascotas.Models;

namespace BE_CRUDMascotas.Repository
{
    public interface IMascotaRepository
    {
        Task<List<Mascota>> GetListMascotas();
        Task<Mascota> GetMascotaById(int id);

        Task DeleteMascota(int id);

        Task<Mascota> PostMascota(Mascota mascota);

        Task EditMascota(Mascota mascota);
    }
}
