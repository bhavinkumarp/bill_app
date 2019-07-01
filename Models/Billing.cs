using System;  
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_core_role_based_authentication_master.Models
{
    public class Billing
    {
        public int Id {get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClaimNumber { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string ProviderName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string DOB { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string DOS { get; set; }
        [Required]
        public string CPT1 { get; set; }        
        public string CPT2 { get; set; }        
        public string CPT3 { get; set; }        
        public string CPT4 { get; set; }
        [Required]
        public string ICD1 { get; set; }        
        public string ICD2 { get; set; }        
        public string ICD3 { get; set; }
    }
}