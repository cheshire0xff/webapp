using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class JobApplication
    {
        [Key]
        public int id { get; set;}
        [Required]
        public int jobOfferId { get; set;}
        [Required]
        public string userId { get; set;}
        [Required]
        public int fileId { get; set;}
        [Required]
        public short accepted { get; set;}
    }
    
}