using miso_greenshop_api.Domain.Models;
using miso_greenshop_api.Dtos.Plants;
using MediatR;

namespace miso_greenshop_api.Application.Commands.Plants
{
    public class AddPlantCommand : IRequest<Plant>
    {
        public PostPlantDto? Plant {  get; set; } 
    }
}
