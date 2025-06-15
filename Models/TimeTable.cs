namespace DynamicTimeTable.Models
{
    public class TimeTable
    {
        public List<string> Subjects { get; set; } = new();
        public int WorkingDays { get; set; }
        public int SubjectsPerDay { get; set; }

        public string[,] Timetable { get; set; }
    }

}
