using miso_greenshop_api.Dtos.Users;
using MediatR;

namespace miso_greenshop_api.Application.Queries.Users
{
    public class GetJwtQuery : IRequest<string>
    {
        public LoginDto? LoginDto { get; set; }
    }
}
