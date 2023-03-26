using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Models;

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

