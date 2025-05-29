using miso_greenshop_api.Dtos.Users;
using MediatR;

namespace miso_greenshop_api.Application.Commands.Users
{
    public class AddUserCommand : IRequest<Unit>
    {
        public RegisterDto? RegisterDto { get; set; }
    }
}
