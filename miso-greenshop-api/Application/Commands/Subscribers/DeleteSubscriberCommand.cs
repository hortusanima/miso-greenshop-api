﻿using MediatR;

namespace miso_greenshop_api.Application.Commands.Subscribers
{
    public class DeleteSubscriberCommand : IRequest<Unit>
    {
        public string? Email { get; set; }
    }
}
