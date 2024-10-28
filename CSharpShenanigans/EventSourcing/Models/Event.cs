namespace CSharpShenanigans.EventSourcing.Models;

public abstract class Event
{
    public abstract Guid StreamId { get; }

    public DateTime CreatedAtUtc { get; set; }
}