using AutoMapper;
using miso_greenshop_api.Application.Commands.Users;
using miso_greenshop_api.Domain.Interfaces.Repositories;
using miso_greenshop_api.Domain.Models;
using MediatR;

namespace miso_greenshop_api.Application.Handlers.Users
{
    public class AddUserHandler(
        IUsersRepository usersRepository,
        ISubscribersRepository subscribersRepository,
        IMapper mapper) : 
        IRequestHandler<AddUserCommand, Unit>
    {
        private readonly IUsersRepository _usersRepository = 
            usersRepository;
        private readonly ISubscribersRepository _subscribersRepository = 
            subscribersRepository;
        private readonly IMapper _mapper = 
            mapper;

        public async Task<Unit> Handle(
            AddUserCommand request, 
            CancellationToken cancellationToken)
        {
            var user = _mapper
                .Map<User>(request.RegisterDto);
            var subscriber = await _subscribersRepository
                .GetSubscriberByEmailAsync(request.RegisterDto!.Email!);

            if (subscriber != null && 
                request.RegisterDto.IsSubscribed == false)
            {
                await _subscribersRepository
                    .DeleteSubscriberAsync(subscriber);
            }

            if (subscriber == null && 
                request.RegisterDto.IsSubscribed == true)
            {
                subscriber = _mapper
                    .Map<Subscriber>(request.RegisterDto);
                await _subscribersRepository
                    .AddSubscriberAsync(subscriber);
            }

            await _usersRepository
                .AddUserAsync(user);

            return Unit.Value;
        }
    }
}
