using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC.Tenants.Application.Tenants.Models
{
    public class ContactDto(string Email, string Phone, string Address)
    {
        public string Email { get; } = Email;
        public string Phone { get; } = Phone;
        public string Address { get; } = Address;
    }
}