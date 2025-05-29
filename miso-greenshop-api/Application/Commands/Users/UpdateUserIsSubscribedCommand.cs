﻿using MediatR;

namespace miso_greenshop_api.Application.Commands.Users
{
    public class UpdateUserIsSubscribedCommand : IRequest<Unit>
    {
        public bool IsSubscribed { get; set; }
    }
}
