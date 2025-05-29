using miso_greenshop_api.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace miso_greenshop_api.Dtos.Subscribers
{
    public class SubscriberDto
    {
        [Required]
        [EmailIsValid]
        public string? SubscriberEmail { get; set; }
    }
}
