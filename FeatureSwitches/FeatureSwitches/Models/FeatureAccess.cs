using System;
using System.Collections.Generic;

#nullable disable

namespace FeatureSwitches.Models
{
    public partial class FeatureAccess
    {
        public decimal FeatureId { get; set; }
        public string FeatureName { get; set; }
        public string UserEmail { get; set; }
        public bool? IsAccessible { get; set; }
    }
}
