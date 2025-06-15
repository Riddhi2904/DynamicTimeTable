namespace DynamicTimeTable.Models
{
    public class SubjectHours
    {
        public int TotalHours { get; set; }

        public List<SubjectHourEntry> Subjects { get; set; } = new();
    }

    public class SubjectHourEntry
    {
        public string SubjectName { get; set; }
        public int Hours { get; set; }
    }

}
