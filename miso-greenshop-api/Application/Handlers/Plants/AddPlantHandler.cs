using AutoMapper;
using miso_greenshop_api.Application.Commands.Plants;
using miso_greenshop_api.Domain.Interfaces.Repositories;
using miso_greenshop_api.Domain.Models;
using MediatR;

namespace miso_greenshop_api.Application.Handlers.Plants
{
    public class AddPlantHandler(
        IPlantsRepository plantsRepository,
        IMapper mapper) : 
        IRequestHandler<AddPlantCommand, Plant>
    {
        private readonly IPlantsRepository _plantsRepository = 
            plantsRepository;
        private readonly IMapper _mapper = 
            mapper;

        public async Task<Plant> Handle(
            AddPlantCommand request, 
            CancellationToken cancellationToken)
        {
            var plant = _mapper
                .Map<Plant>(request.Plant);

            return await _plantsRepository
                .AddPlantAsync(plant);
        }
    }
}
