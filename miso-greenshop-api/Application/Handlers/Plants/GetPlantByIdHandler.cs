using AutoMapper;
using miso_greenshop_api.Application.Queries.Plants;
using miso_greenshop_api.Domain.Interfaces.Repositories;
using miso_greenshop_api.Dtos.Plants;
using MediatR;

namespace miso_greenshop_api.Application.Handlers.Plants
{
    public class GetPlantByIdHandler(
        IPlantsRepository plantsRepository,
        IMapper mapper) : 
        IRequestHandler<GetPlantByIdQuery, GetPlantDto>
    {
        private readonly IPlantsRepository _plantsRepository = 
            plantsRepository;
        private readonly IMapper _mapper = 
            mapper;

        public async Task<GetPlantDto> Handle(
            GetPlantByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var plant = await _plantsRepository
                .GetPlantByIdAsync(request.Id!);

            if(!request.Authorized)
            {
                plant!.Price = Math.Truncate(100 *
                (double)plant.Price! /
                (100 + plant.Sale_Percent -
                plant.Sale_Percent_Private)) +
                0.99;
            }

            return _mapper
                .Map<GetPlantDto>(plant);
        }
    }
}
