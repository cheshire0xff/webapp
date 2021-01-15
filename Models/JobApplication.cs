using System;
using System.ComponentModel.DataAnnotations;

namespace CoreApp.Models
{
    public class JobApplication
    {
        [Key]
        public int id { get; set;}
        [Required]
        public int jobOfferId { get; set;}
        [Required]
        public int userId { get; set;}
        [Required]
        public string cvLocation { get; set;}
    }
    
}