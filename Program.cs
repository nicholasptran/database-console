using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

public class Grade
{
    public int GradeId { get; set; }
    public string GradeName { get; set; }
    public string Section { get; set; }

    public ICollection<Student> Students { get; set; }
}

public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = default!;
}

namespace DatabaseConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                // check for null input
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Enter the student's name: ");
                        var name = Console.ReadLine();

                        Console.WriteLine("Enter their height: ");
                        var height = Console.ReadLine();

                        Console.WriteLine("Enter their weight");
                        var weight = Console.ReadLine();

                        if (name != null && height != null && weight != null)
                        {
                            var student = new Student()
                            {
                                // uppercase first letter
                                StudentName = (char.ToUpper(name[0]) + name.Substring(1)),
                                Height = Convert.ToDecimal(height),
                                Weight = Convert.ToSingle(weight)
                            };

                            context.Students.Add(student);
                            context.SaveChanges();
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Cannot be empty");
                        continue;
                    }
                }
                var studentQuery = context.Students
                                   .AsEnumerable();

                Console.WriteLine("Current students: ");
                // Console.WriteLine(studentQuery);
                foreach (var item in studentQuery)
                {
                    Console.WriteLine($"Name: {item.StudentName}  Height: {item.Height}  Weight: {item.Weight}");
                }
            }
        }
    }
}

public class SchoolContext : DbContext
{
    string? ConnString = Environment.GetEnvironmentVariable("CONN_STRING");

    public DbSet<Student> Students { get; set; } = default!;
    public DbSet<Grade> Grades { get; set; } = default!;
    public DbSet<Course> Courses { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnString);
    }
}

