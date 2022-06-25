using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.Services.Core.Data.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
