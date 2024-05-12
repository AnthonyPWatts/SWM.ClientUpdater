using SWM.ClientUpdater.Application.Enums;
using SWM.ClientUpdater.Application.ValueObjects;

namespace SWM.ClientUpdater.Application.DTOs;

// This DTO is used to transfer Client data between layers of the application.
public record  ClientDto(
    int UserId, 
    string Name,
    OnboardingStatus OnboardingStatus, 
    DateOnly JoinedDate, 
    Address? Address, 
    MaritalStatus MaritalStatus);
