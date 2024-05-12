using SWM.ClientUpdater.Application.DTOs;
using SWM.ClientUpdater.Application.Enums;
using SWM.ClientUpdater.Application.ValueObjects;

namespace SWM.ClientUpdater.Database;

internal class ClientDbObject
{
    // These should all match the db table schema so probably need renaming
    public int ID;
    public string Name = null!;
    public string OnboardingStatus = null!;
    public DateTime JoinedDate;
    public string? Address;
    public string? MaritalStatus;

    public ClientDto ToDto()
    {
        return new ClientDto(
            ID,
            Name,
            Enum.TryParse<OnboardingStatus>(OnboardingStatus, ignoreCase: true, out var parsedStatus)
                ? parsedStatus
                : Application.Enums.OnboardingStatus.Pending,
            new DateOnly(JoinedDate.Year, JoinedDate.Month, JoinedDate.Day),
            new Address() { AddressLines = Address?.Split(',') ?? [] },
            MaritalStatus is not null 
                ? Enum.Parse<MaritalStatus>(MaritalStatus, ignoreCase: true) 
                : Application.Enums.MaritalStatus.Unknown
        );
    }
}
