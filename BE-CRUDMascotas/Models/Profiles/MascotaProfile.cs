using AutoMapper;
using BE_CRUDMascotas.Models.DTOs;
using Microsoft.Identity.Client;
using System.Runtime;

namespace BE_CRUDMascotas.Models.Profiles

{
    public class MascotaProfile: Profile
    {
        public MascotaProfile() { 
            CreateMap<Mascota, MascotaDTO>();
            CreateMap<MascotaDTO, Mascota>();
        }
    }
}
