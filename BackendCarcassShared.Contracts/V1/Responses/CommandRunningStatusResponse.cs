namespace BackendCarcassShared.Contracts.V1.Responses;

public sealed class CommandRunningStatusResponse
{
    public string? CommandKey { get; set; }
    public bool Finished { get; set; }
    public int ProgressMaxValue { get; set; }
    public int ProgressValue { get; set; }
    public bool Success { get; set; }
}
