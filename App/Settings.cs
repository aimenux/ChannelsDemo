namespace App;

public sealed record Settings
{
    public int ItemsNumber { get; init; }
    public int ConsumerDelay { get; init; }
    public int ProducerDelay { get; init; }
    public int BoundedChannelSize { get; init; }
}