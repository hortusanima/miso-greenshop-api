using miso_greenshop_api.Dtos.Subscribers;
using MediatR;

namespace miso_greenshop_api.Application.Queries.Subscribers
{
    public class GetAllSubscribersQuery() : IRequest<List<SubscriberDto>>
    {
    }
}
