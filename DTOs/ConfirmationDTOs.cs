using BookingAPI.Models;

namespace BookingAPI.DTOs
{
    public class ConfirmationDTO
    {
        public int Id {get; set;}
        public int BookingId {get; set;}
        public string FullName {get; set;} = string.Empty;
        public string CompanyName {get; set;} = string.Empty;
        public string HelpWith {get; set;} = string.Empty;
        public MeetingType Meeting {get; set;} = MeetingType.InPerson;
        public DateOnly BookingDate {get; set;}
        public TimeOnly BookingTime {get; set;}
    }
}