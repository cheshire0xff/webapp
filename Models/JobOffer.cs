using System;
using System.ComponentModel.DataAnnotations;

namespace CoreApp.Models
{
    public class JobOffer
    {
        [Key]
        public int id { get; set;}
        [Required]
        public int employerId { get; set;}
        [Required]
        public string contentLocation { get; set;}
        [Required]
        public string localization { get; set;}
        public string tags { get; set;}
        public DateTime expirationDate { get; set;}
    }
    
}