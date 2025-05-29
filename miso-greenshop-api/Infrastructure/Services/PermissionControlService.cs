﻿using miso_greenshop_api.Application.Models;
using miso_greenshop_api.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace miso_greenshop_api.Infrastructure.Services
{
    public class PermissionControlService(IOptions<PermissionControlOptions> permissionControlOptions) 
        : IPermissionControlService
    {
        private readonly PermissionControlOptions _permissionControlOptions = 
            permissionControlOptions.Value;
        private readonly ILogger<IPermissionControlService> _logger;
        public bool VerifyApplication(string applicationKey)
        {
            _logger.LogInformation(_permissionControlOptions.ApplicationKey);
            _logger.LogInformation(applicationKey);
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
