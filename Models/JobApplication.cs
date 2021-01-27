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
        public enum Status
        {
            Pending,
            Rejected,
            Accepted
        }
        [Required]
        public Status status{ get; set;}
    }
    
}