using miso_greenshop_api.Application.Queries.Users;
using miso_greenshop_api.Domain.Interfaces.Jwt;
using miso_greenshop_api.Domain.Interfaces.Repositories;
using MediatR;

namespace miso_greenshop_api.Application.Handlers.Users
{
    public class GetJwtHandler(
        IUsersRepository usersRepository, 
        IJwtService jwtService) : 
        IRequestHandler<GetJwtQuery, string>
    {
        private readonly IUsersRepository _usersRepository = 
            usersRepository;
        private readonly IJwtService _jwtService = 
            jwtService;

        public async Task<string> Handle(
            GetJwtQuery request, 
            CancellationToken cancellationToken)
        {
            var user = await _usersRepository
                .GetUserByEmailAsync(request.LoginDto!.Email!);

            return _jwtService
                .Generate(user!.UserId!);
        }
    }
}
