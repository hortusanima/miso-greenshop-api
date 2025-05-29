using miso_greenshop_api.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace miso_greenshop_api.Domain.Models
{
    public class Subscriber
    {
        [Key]
        public string? SubscriberId { get; set; }
        [Required]
        [EmailIsValid]
        public string? SubscriberEmail { get; set; }
    }
}
