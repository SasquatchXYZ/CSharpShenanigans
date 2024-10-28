using CSharpShenanigans.EventSourcing.Models;

namespace CSharpShenanigans.EventSourcing;

public static class TestEventSourcing
{
    public static void TestStudentDatabase()
    {
        var studentDatabase = new StudentDatabase();

        var studentId = Guid.Parse("410efa39-917b-45d4-83ff-f9a618d760a3");

        var studentCreated = new StudentCreated
        {
            StudentId = studentId,
            Email = "nick@dometrain.com",
            FullName = "Nick Chapsas",
            DateOfBirth = new DateTime(1993, 01, 01),
        };

        studentDatabase.Append(studentCreated);

        var studentEnrolled = new StudentEnrolled
        {
            StudentId = studentId,
            CourseName = "From Zero to Hero: REST APIs in .NET"
        };

        studentDatabase.Append(studentEnrolled);

        var studentUpdated = new StudentUpdated
        {
            StudentId = studentId,
            Email = "nickchapsas@dometrain.com",
            FullName = "Nick Chapsas",
        };


        studentDatabase.Append(studentUpdated);

        var studentEnrolledAgain = new StudentEnrolled
        {
            StudentId = studentId,
            CourseName = "Getting Started with Event Sourcing in .NET"
        };

        studentDatabase.Append(studentEnrolledAgain);

        var student = studentDatabase.GetStudent(studentId);

        Console.WriteLine("Student:");
        Console.WriteLine(student?.ToString() ?? "No student found");

        // var studentFromView = studentDatabase.GetStudentView(studentId);

        // Console.WriteLine("Student From View:");
        // Console.WriteLine(studentFromView?.ToString() ?? "No student found");
    }
}