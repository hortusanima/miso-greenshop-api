using MediatR;

namespace miso_greenshop_api.Application.Queries.Reviews
{
    public class GetNumberOfReviewsByRatingsQuery : IRequest<Dictionary<int, int>>
    {
        public string? PlantId { get; set; }
    }
}
