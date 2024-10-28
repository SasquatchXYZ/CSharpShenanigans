namespace CSharpShenanigans.EventSourcing.Models;

public class Student
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public List<string> EnrolledCourses { get; set; } = new();

    public DateTime DateOfBirth { get; set; }

    private void Apply(StudentCreated studentCreated)
    {
        Id = studentCreated.StudentId;
        FullName = studentCreated.FullName;
        Email = studentCreated.Email;
        DateOfBirth = studentCreated.DateOfBirth;
    }

    private void Apply(StudentUpdated updated)
    {
        FullName = updated.FullName;
        Email = updated.Email;
    }

    private void Apply(StudentEnrolled enrolled)
    {
        if (!EnrolledCourses.Contains(enrolled.CourseName))
        {
            EnrolledCourses.Add(enrolled.CourseName);
        }
    }

    private void Apply(StudentUnEnrolled unenrolled)
    {
        if (EnrolledCourses.Contains(unenrolled.CourseName))
        {
            EnrolledCourses.Remove(unenrolled.CourseName);
        }
    }

    public void Apply(Event @event)
    {
        switch (@event)
        {
            case StudentCreated studentCreated:
                Apply(studentCreated);
                break;
            case StudentUpdated studentUpdated:
                Apply(studentUpdated);
                break;
            case StudentEnrolled studentEnrolled:
                Apply(studentEnrolled);
                break;
            case StudentUnEnrolled studentUnEnrolled:
                Apply(studentUnEnrolled);
                break;
        }
    }

    public override string ToString()
    {
        return $"""
                Full Name: {FullName}
                Email: {Email}
                DateOfBirth: {DateOfBirth}
                Student Id: {Id}
                Courses: {string.Join("\n\t ", EnrolledCourses)}
                """;
    }
}