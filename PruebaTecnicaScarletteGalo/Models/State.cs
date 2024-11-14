
using System;
namespace PruebaTecnicaScarletteGalo.Models

{
    public class State
    {
        public Guid StateId { get; set; } // UUID como clave primaria
        public string StateName { get; set; } // Nombre del estado (Active, Inactive, etc.)

        // Navegación
        public ICollection<User> Users { get; set; }
        public ICollection<Space> Spaces { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
