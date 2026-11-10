using System;

namespace BackendCarcassShared.Contracts.Tests.TestDoubles;

//დრო, რომელსაც ტესტი თვითონ აგრძელებს
internal sealed class ManualTimeProvider : TimeProvider
{
    private DateTimeOffset _now;

    public ManualTimeProvider(DateTimeOffset now)
    {
        _now = now;
    }

    public override DateTimeOffset GetUtcNow()
    {
        return _now;
    }

    public void Advance(TimeSpan by)
    {
        _now += by;
    }
}
