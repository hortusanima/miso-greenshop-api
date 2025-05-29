using MediatR;

namespace miso_greenshop_api.Application.Queries.Reviews
{
    public class GetTotalNumberOfReviewsByPlantIdQuery : IRequest<int>
    {
        public string? PlantId { get; set; }
    }
}
