using miso_greenshop_api.Dtos.Reviews;
using MediatR;

namespace miso_greenshop_api.Application.Queries.Reviews
{
    public class GetReviewByPlantIdForUserQuery : IRequest<GetReviewDto?>
    {
        public string? PlantId { get; set; }
    }
}
