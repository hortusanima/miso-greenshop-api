using AutoMapper;
using miso_greenshop_api.Application.Queries.Subscribers;
using miso_greenshop_api.Domain.Interfaces.Repositories;
using miso_greenshop_api.Dtos.Subscribers;
using MediatR;

namespace miso_greenshop_api.Application.Handlers.Subscribers
{
    public class GetSubscriberByEmailHandler(
        ISubscribersRepository subscribersRepository, 
        IMapper mapper) : 
        IRequestHandler<GetSubscriberByEmailQuery, SubscriberDto>
    {
        private readonly ISubscribersRepository _subscribersRepository = 
            subscribersRepository;
        private readonly IMapper _mapper = 
            mapper;

        public async Task<SubscriberDto> Handle(
            GetSubscriberByEmailQuery request, 
            CancellationToken cancellationToken)
        {
            var subscriber = await _subscribersRepository
                .GetSubscriberByEmailAsync(request.Email!);

            return _mapper
                .Map<SubscriberDto>(subscriber);
        }
    }
}
