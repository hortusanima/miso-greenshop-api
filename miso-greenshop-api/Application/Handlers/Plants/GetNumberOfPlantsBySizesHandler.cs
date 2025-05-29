﻿using miso_greenshop_api.Application.Queries.Plants;
using miso_greenshop_api.Domain.Interfaces.Repositories;
using MediatR;
using static miso_greenshop_api.Domain.Models.Enums.SizeEnum;

namespace miso_greenshop_api.Application.Handlers.Plants
{
    public class GetNumberOfPlantsBySizesHandler(IPlantsRepository plantsRepository) : 
        IRequestHandler<GetNumberOfPlantsBySizesQuery, Dictionary<string, int>>
    {
        private readonly IPlantsRepository _plantsRepository = 
            plantsRepository;

        public async Task<Dictionary<string, int>> Handle(
            GetNumberOfPlantsBySizesQuery request, 
            CancellationToken cancellationToken)
        {
            var sizeCounts = new Dictionary<string, int>();

            var smallCount = await _plantsRepository
                .GetNumberOfPlantsBySizeAsync(Size.S);
            var mediumCount = await _plantsRepository
                .GetNumberOfPlantsBySizeAsync(Size.M);
            var largeCount = await _plantsRepository
                .GetNumberOfPlantsBySizeAsync(Size.L);
            var extraLargeCount = await _plantsRepository
                .GetNumberOfPlantsBySizeAsync(Size.XL);

            sizeCounts["Small"] = smallCount;
            sizeCounts["Medium"] = mediumCount;
            sizeCounts["Large"] = largeCount + 
                extraLargeCount;

            return sizeCounts;
        }
    }
}
