using SWM.ClientUpdater.Application.DTOs;
using SWM.ClientUpdater.Application.Enums;
using SWM.ClientUpdater.Application.ValueObjects;

namespace SWM.ClientUpdater.Application.Models;

// This Client model is used to represent a Client in the application logic
// Only the Dto should be used for comms, so this is marked as internal.
internal sealed class Client
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public OnboardingStatus OnboardingStatus { get; set; }
    public DateOnly JoinedDate { get; set; }
    public Address? Address { get; set; }
    public MaritalStatus MaritalStatus { get; set; }

    public ClientDto ToDto()
    {
        return new ClientDto(UserId, Name, OnboardingStatus, JoinedDate, Address, MaritalStatus);
    }
}
