using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager.Services.Core.Service.Models.Requests
{
    public class BaseRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
