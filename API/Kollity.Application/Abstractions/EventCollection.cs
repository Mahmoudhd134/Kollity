using Kollity.Contracts;
using Kollity.Contracts.Events;

namespace Kollity.Application.Abstractions;

public class EventCollection
{
    private readonly List<IEvent> _events = [];

    public bool Any() => _events.Count != 0;
    public IReadOnlyList<IEvent> Events() => _events.ToList();
    public void Clear() => _events.Clear();
    public void Raise(IEvent e) => _events.Add(e);
}