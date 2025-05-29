﻿using System.ComponentModel.DataAnnotations;

namespace miso_greenshop_api.Domain.Models
{
    public class CartItem
    {
        [Required]
        public string? CartId { get; set; }
        public Cart? Cart { get; set; }
        [Required]
        public string? PlantId { get; set; }
        public Plant? Plant { get; set; }
        [Required]
        [Range(1, 20, ErrorMessage = "Quantity must be greater than 0 and cannot be over 20.")]
        public int Quantity { get; set; }
    }
}
