﻿using System.ComponentModel.DataAnnotations;

namespace miso_greenshop_api.Domain.Models
{
    public class Cart
    {
        [Key]
        public string? CartId { get; set; }
        [Required]
        public string? UserId { get; set; }
        public User? User { get; set; }
        public List<CartItem> CartItems { get; set; } = [];
        [Range(0.01, double.MaxValue, ErrorMessage = "Cart Price must be greater than 0.")]
        public double CartPrice { get; set; }
    }
}
