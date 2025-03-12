using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC.Tenants.Models;

namespace VC.Tenants.Application.Tenants.Models
{
    public class TenantConfigurationDto(string about, string currency, string language, string timeZoneId)
    {
        public string About { get; } = about;

        public string Currency { get; } = currency;

        public string Language { get; } = language;

        public string TimeZoneId { get; } = timeZoneId;
    }
}