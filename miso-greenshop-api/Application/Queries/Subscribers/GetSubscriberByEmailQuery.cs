using miso_greenshop_api.Dtos.Subscribers;
using MediatR;

namespace miso_greenshop_api.Application.Queries.Subscribers
{
    public class GetSubscriberByEmailQuery : IRequest<SubscriberDto>
    {
        public string? Email { get; set; }
    }
}
