﻿using miso_greenshop_api.Application.Models;
using miso_greenshop_api.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace miso_greenshop_api.Infrastructure.Services
{
    public class PermissionControlService(IOptions<PermissionControlOptions> permissionControlOptions, ILogger<PermissionControlService> logger) 
        : IPermissionControlService
    {
        private readonly PermissionControlOptions _permissionControlOptions = 
            permissionControlOptions.Value;
        private readonly ILogger<PermissionControlService> _logger = logger;

        public bool VerifyApplication(string applicationKey)
        {
            if (applicationKey == 
                _permissionControlOptions.ApplicationKey)
            {
                return true;
            }
            return false;
        }

        public bool VerifyAdmin(string adminKey)
        {
            if (adminKey == 
                _permissionControlOptions.AdminKey)
            {
                return true;
            }
            return false;
        }
    }
}
