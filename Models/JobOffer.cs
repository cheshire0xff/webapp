using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class JobOffer
    {
        [Key]
        public int id { get; set;}
        [Required]
        public string employerId { get; set;}
        [Required]
        public string description { get; set;}
        [Required]
        public int fileId{ get; set;}
        [Required]
        public string localization { get; set;}
        public string tags { get; set;}
        public DateTime expirationDate { get; set;}
    }
    
}