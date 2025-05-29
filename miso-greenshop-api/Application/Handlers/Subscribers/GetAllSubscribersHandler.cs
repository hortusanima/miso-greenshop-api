using AutoMapper;
using miso_greenshop_api.Application.Queries.Subscribers;
using miso_greenshop_api.Domain.Interfaces.Repositories;
using miso_greenshop_api.Dtos.Subscribers;
using MediatR;

namespace miso_greenshop_api.Application.Handlers.Subscribers
{
    public class GetAllSubscribersHandler(
        ISubscribersRepository subscribersRepository,
        IMapper mapper) : 
        IRequestHandler<GetAllSubscribersQuery, List<SubscriberDto>>
    {
        private readonly ISubscribersRepository _subscribersRepository = 
            subscribersRepository;
        private readonly IMapper _mapper = 
            mapper;

        public async Task<List<SubscriberDto>> Handle(
            GetAllSubscribersQuery request, 
            CancellationToken cancellationToken)
        {
            var subscribers = await _subscribersRepository
                .GetAllSubscribersAsync();

            return _mapper
                .Map<List<SubscriberDto>>(subscribers);
        }
    }
}
