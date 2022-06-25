using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager.Services.Core.Service.Models.Custom
{
    public class APIResponse
    {
        public dynamic Data { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public string DisplayMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
