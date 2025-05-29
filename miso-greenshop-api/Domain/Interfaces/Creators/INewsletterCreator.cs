using miso_greenshop_api.Application.Models;
using System.Net.Mail;

namespace miso_greenshop_api.Domain.Interfaces.Creators
{
    public interface INewsletterCreator
    {
        MailMessage CreateNewsletter(
            string from, 
            NewsletterHeader header);
    }
}
