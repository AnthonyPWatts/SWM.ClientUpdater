namespace SWM.ClientUpdater.Application.ValueObjects;

public sealed class Address
{
    public string[] AddressLines { get; set; } = null!;

    public string ToSingleLine()
    {
        return string.Join(", ", AddressLines);
    }
}
