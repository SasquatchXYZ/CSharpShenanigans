using CSharpShenanigans.EventSourcing.Models;

namespace CSharpShenanigans.EventSourcing;

public class StudentDatabase
{
    private readonly Dictionary<Guid, SortedList<DateTime, Event>> _studentEvents = new();
    private readonly Dictionary<Guid, Student> _students = new();

    public void Append(Event @event)
    {
        var stream = _studentEvents!.GetValueOrDefault(@event.StreamId, null);
        @event.CreatedAtUtc = DateTime.UtcNow;

        if (stream is null)
        {
            stream = new SortedList<DateTime, Event>();
            stream.Add(@event.CreatedAtUtc, @event);
            _studentEvents.Add(@event.StreamId, stream);
            return;
        }

        _studentEvents[@event.StreamId].Add(@event.CreatedAtUtc, @event);
        _students[@event.StreamId] = GetStudent(@event.StreamId)!;
    }

    public Student? GetStudent(Guid studentId)
    {
        if (!_studentEvents.TryGetValue(studentId, out var studentEvents))
        {
            return null;
        }

        var student = new Student();
        foreach (var @event in studentEvents.Values)
        {
            student.Apply(@event);
        }

        return student;
    }

    public Student? GetStudentView(Guid studentId)
    {
        return _students!.GetValueOrDefault(studentId, null);
    }
}
