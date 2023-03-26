namespace Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public byte[]? Photo { get; set; }
        public decimal Height { get; set; }
        public float Weight { get; set; }

        public Grade? Grade { get; set; }
    }
}