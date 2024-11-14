
using System;
namespace PruebaTecnicaScarletteGalo.Models
{
    public class User
    {
        public Guid UserId { get; set; } // UUID como clave primaria
        public string Username { get; set; } // Nombre de usuario
        public string Password { get; set; } // Contraseña (encriptar antes de guardar)
        public string Role { get; set; } // Rol del usuario (Member o Employee)
        public Guid StateId { get; set; } // Relación con la tabla States

        // Navegación
        public State State { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
