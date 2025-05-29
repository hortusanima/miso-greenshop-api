using MediatR;

namespace miso_greenshop_api.Application.Commands.Plants
{
    public class DeletePlantCommand : IRequest<Unit>
    {
        public string? Id { get; set; }
    }
}
