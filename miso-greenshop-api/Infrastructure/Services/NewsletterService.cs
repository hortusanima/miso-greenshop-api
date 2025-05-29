using miso_greenshop_api.Application.Models;
using miso_greenshop_api.Domain.Interfaces.Creators;
using miso_greenshop_api.Domain.Interfaces.Service;
using miso_greenshop_api.Infrastructure.Creators;
using miso_greenshop_api.Infrastructure.Newsletter;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace miso_greenshop_api.Infrastructure.Services
{
    public class NewsletterService : INewsletterService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _smtpUsername;
        private readonly SmtpClient _smtpClient;
        private readonly Dictionary<string, Type> _newsletterTypeMap;

        public NewsletterService(
            IServiceProvider serviceProvider,
            IOptions<SmtpOptions> smtpOptions,
            SmtpClient smtpClient)
        {
            _serviceProvider = serviceProvider;
            _smtpUsername = smtpOptions.Value.Username!;
            _smtpClient = smtpClient;
            _newsletterTypeMap = new Dictionary<string, Type>
            {
                { "registration", typeof(RegistrationNewsletterCreator) },
                { "newPlant", typeof(NewPlantNewsletterCreator) },
                { "subscription", typeof(SubscriptionNewsletterCreator) },
                { "purchase", typeof(PurchaseNewsletterCreator) }
            };
        }

        public async Task SendNewsletterAsync(
            string type, 
            NewsletterHeader header)
        {
            var newsletterType = _newsletterTypeMap[type];
            INewsletterCreator creator = (INewsletterCreator)
                ActivatorUtilities.CreateInstance(
                    _serviceProvider, 
                    newsletterType);

            MailMessage newsletter = creator
                .CreateNewsletter(
                _smtpUsername, 
                header);

            try
            {
                await _smtpClient
                    .SendMailAsync(newsletter);
                Console.WriteLine(
                    "Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Exception: {ex.Message}");
            }
        }
    }
}
