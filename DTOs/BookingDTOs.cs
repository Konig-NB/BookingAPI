using System.ComponentModel.DataAnnotations;
using BookingAPI.Models;

namespace BookingAPI.DTOs
{
    public class CreateBookingDTO
    {
        [Required, StringLength(100)]
        public string FullName {get; set;} = string.Empty;
        
        [StringLength(100)]
        public string? JobTitle {get; set;}

        [Required, StringLength(100)]
        public string CompanyName {get; set;} = string.Empty;

        [Required,EmailAddress]
        public string Email {get; set;} = string.Empty;

        [Required, StringLength(100)]
        public string Industry {get; set;} = string.Empty;

        [Required, StringLength(100)]
        public string HelpWith {get; set;} = string.Empty;

        [Required, StringLength(500)]
        public string ProblemDescription {get; set;} = string.Empty;

        [Required, StringLength(500)]
        public string SessionGoal {get; set;} = string.Empty;

        [Required]
        public MeetingType Meeting {get; set;} = MeetingType.InPerson;

        [Required]
        public DateOnly Date {get; set;}

        [Required]
        public TimeOnly Time {get; set;}
        public bool ContactPermission {get; set;} = false;
    }

    public class BookingDTO
    {
        public int Id {get; set;}
        public string FullName {get; set;} = string.Empty;
        public string? JobTitle {get; set;}
        public string CompanyName {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;
        public string Industry {get; set;} = string.Empty;
        public string HelpWith {get; set;} = string.Empty;
        public string ProblemDescription {get; set;} = string.Empty;
        public string SessionGoal {get; set;} = string.Empty;
        public MeetingType Meeting {get; set;} = MeetingType.InPerson;
        public DateOnly Date {get; set;}
        public TimeOnly Time {get; set;}
        public bool ContactPermission {get; set;} = false;
    }
}