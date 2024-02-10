using Kollity.Application.Dtos.Identity;

namespace Kollity.Application.Commands.Identity.ChangeImagePhoto;

public record ChangeUserProfileImageCommand(ChangeImagePhotoDto ImageDto) : ICommand;