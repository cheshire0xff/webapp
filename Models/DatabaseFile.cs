using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class DatabaseFile 
    {
        [Key]
        public int Id { get; set;}
        [Required]
        public string UserId { get; set;}
        [Required]
        public byte[] Content { get; set;}
        [Required]
        public byte[] Hash { get; set;}
    }
    
}