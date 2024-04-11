using Kollity.Services.Application.Dtos.Identity;

namespace Kollity.Services.Application.Commands.Identity.ChangeImagePhoto;

public record ChangeUserProfileImageCommand(ChangeImagePhotoDto ImageDto) : ICommand;