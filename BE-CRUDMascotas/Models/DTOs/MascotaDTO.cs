﻿namespace BE_CRUDMascotas.Models.DTOs
{
    //Implementacion DTOs
    public class MascotaDTO
    {

        public int Id { get; set; }
        public string Raza { get; set; }
        public string Color { get; set; }
        public float Peso { get; set; }

        public string Nombre { get; set; }
        public int Edad { get; set; }
    }
}
