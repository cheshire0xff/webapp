using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public enum Status
    {
        Pending,
        Rejected,
        Accepted
    }
    public class JobApplication
    {
        [Key]
        public int Id { get; set;}
        [Required]
        public int JobOfferId { get; set;}
        [Required]
        public string UserId { get; set;}
        [Required]
        public int FileId { get; set;}
        [Required]
        public Status Status{ get; set;}
    }
    
}