using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SystemTools.SharedKernel;

namespace BackendCarcassShared.Contracts.Tests.TestDoubles;

//JwtContractReCounterApiClient-ის კონსტრუქტორი protected-ია, ტესტებს კი კლასის ეგზემპლარი სჭირდებათ
internal sealed class TestableJwtContractReCounterApiClient : JwtContractReCounterApiClient
{
    public TestableJwtContractReCounterApiClient(ILogger logger, IHttpClientFactory httpClientFactory, string server,
        bool useConsole) : base(logger, httpClientFactory, server, useConsole)
    {
    }

    //მოთხოვნა, რომელიც შეტყობინებების hub-საც უშვებს, როგორც მემკვიდრე კლიენტების (მაგალითად, AppGrammarGeApiClient)
    //პროცესების გამშვები მეთოდები
    public ValueTask<Result> PostWithMessageHub(string afterServerAddress,
        CancellationToken cancellationToken = default)
    {
        return PostAsync(afterServerAddress, cancellationToken);
    }
}
