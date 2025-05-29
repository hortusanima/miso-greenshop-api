using miso_greenshop_api.Application.Models;

namespace miso_greenshop_api.Domain.Interfaces.Service
{
    public interface INewsletterService
    {
        Task SendNewsletterAsync(
            string newsletterType, 
            NewsletterHeader message);
    }
}
