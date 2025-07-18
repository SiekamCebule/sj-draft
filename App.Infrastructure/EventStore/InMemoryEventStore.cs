using App.Application.Abstractions;
using App.Domain.Shared;

namespace App.Infrastructure.EventStore;

public class InMemoryEventStore<TId, TPayload> : IEventStore<TId, TPayload> where TId : notnull
{
    private readonly Dictionary<TId, List<DomainEvent<TPayload>>> _store = new();

    public Task AppendAsync(
        TId id,
        IReadOnlyList<DomainEvent<TPayload>> events,
        int expectedVersion,
        CancellationToken ct)
    {
        if (!_store.TryGetValue(id, out var stream))
        {
            stream = new List<DomainEvent<TPayload>>();
            _store[id] = stream;
        }

        if (stream.Count != expectedVersion)
            throw new InvalidOperationException("Wrong expected version");

        stream.AddRange(events);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<DomainEvent<TPayload>>> LoadAsync(TId id, CancellationToken ct)
    {
        _store.TryGetValue(id, out var events);
        return Task.FromResult<IReadOnlyList<DomainEvent<TPayload>>>(events ?? []);
    }
}