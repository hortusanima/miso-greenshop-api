using AutoMapper;
using miso_greenshop_api.Application.Queries.Users;
using miso_greenshop_api.Domain.Interfaces.Repositories;
using miso_greenshop_api.Dtos.Users;
using MediatR;

namespace miso_greenshop_api.Application.Handlers.Users
{
    public class GetAllUsersHandler(
        IUsersRepository usersRepository, 
        IMapper mapper) : 
        IRequestHandler<GetAllUsersQuery, List<GetUserDto>>
    {
        private readonly IUsersRepository _usersRepository = 
            usersRepository;
        private readonly IMapper _mapper = 
            mapper;   

        public async Task<List<GetUserDto>> Handle(
            GetAllUsersQuery request, 
            CancellationToken cancellationToken)
        {
            var users = await _usersRepository
                .GetAllUsersAsync();

            return _mapper
                .Map<List<GetUserDto>>(users);
        }
    }
}
