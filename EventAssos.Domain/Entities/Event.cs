using EventAssos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Place { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int NbMin { get; set; }
        public int NbMax { get; set; }
        public EventStatus Status { get; set; }
        public DateOnly CreationDate { get; set; }

        public DateTime LimiteInscription { get; set; }

        public DateTime Update { get; set; }
        public bool WaitingActiveList { get; set; }

        public byte[]? Img { get; set; }


        //Navigation 
        public Guid? CreatedByUserId { get; set; }
        public User? CreatedBy { get; set; }
        public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
        public ICollection<Category> Categories { get; set; } = new List<Categorie>();
    }
}
}
