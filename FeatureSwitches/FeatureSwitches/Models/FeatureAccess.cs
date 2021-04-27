using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FeatureSwitches.Models
{
    public partial class FeatureAccess
    {
        [Key]
        public int Id { get; set; }
        [Column("featureName")]
        [Display(Name = "Feature Name")]
        [Required(ErrorMessage = "The Feature Name is required")]
        public string FeatureName { get; set; }
        [Column("email")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Column("enable")]
        [Display(Name = "CanAccess")]
        public bool? Enable { get; set; }
    }
}
