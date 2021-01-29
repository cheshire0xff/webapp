using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public enum EmployementType
    {
        FullTime,
        PartTime,
        Apprentice,
        Commision,
    }
    public class JobOffer
    {
        [Key]
        public int Id { get; set;}
        [Required]
        public string EmployerId { get; set;}
        [Required]
        public string Description { get; set;}
        [Required]
        public int FileId{ get; set;}
        [Required]
        public string Localization { get; set;}
        public string Tags { get; set;}
        [Required]
        public DateTime ExpirationDate { get; set;}
        [Required]
        public DateTime AddedDate{ get; set;}

        [Required]
        public EmployementType EmployementType { get; set;}
    }
    
}