using AutoMapper;
using AccountsManager.Services.Core.Service.Contracts;
using AccountsManager.Services.Core.Data.Contracts;
using AccountsManager.Services.Core.Data.Repositories;
using AccountsManager.Services.Core.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AccountsManager.Services.Core.Service.Services
{
    public class RequestInfoService : IRequestInfoService
    {
        public Guid UserId { get; }

        public RequestInfoService()
        {
            // Authenticated User ID to be extracted from HttpContext
            UserId = new Guid("d81d32ba-841e-4c9c-ba9c-50f532845b4f");
        }
    }
}
