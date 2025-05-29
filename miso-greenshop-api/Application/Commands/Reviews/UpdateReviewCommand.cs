﻿using miso_greenshop_api.Dtos.Reviews;
using MediatR;

namespace miso_greenshop_api.Application.Commands.Reviews
{
    public class UpdateReviewCommand : IRequest<Unit>
    {
        public string? PlantId { get; set; }
        public PostReviewDto? Review { get; set; }
    }
}
