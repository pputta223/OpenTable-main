using System;
using System.ComponentModel.DataAnnotations;

namespace OpenTable.Models.DomainModels
{
    public enum ReservationType
    {
        Hold,
        Final
    }
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string TimeSlot { get; set; } = "";

        [Required]
        public int PartySize { get; set; }

        public Restaurant? Restaurant { get; set; } // navigation property

        public ReservationType Status { get; set; } = ReservationType.Hold;
    }
}
