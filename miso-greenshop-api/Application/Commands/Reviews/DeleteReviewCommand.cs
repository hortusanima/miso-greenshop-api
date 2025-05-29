using MediatR;

namespace miso_greenshop_api.Application.Commands.Reviews
{
    public class DeleteReviewCommand : IRequest<Unit>
    {
        public string? PlantId { get; set; }
    }
}
