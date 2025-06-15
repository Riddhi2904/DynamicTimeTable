using System.ComponentModel.DataAnnotations;

namespace DynamicTimeTable.Models
{
    public class inputHour
    {
        [Range(1, 7)]
        public int WorkingDays { get; set; }

        [Range(1, 8)]
        public int SubjectsPerDay { get; set; }

        [Range(1, int.MaxValue , ErrorMessage = "TotalSubjects must be a positive value.")]
        public int TotalSubjects { get; set; }

        public int TotalHours => WorkingDays * SubjectsPerDay;
    }

}
