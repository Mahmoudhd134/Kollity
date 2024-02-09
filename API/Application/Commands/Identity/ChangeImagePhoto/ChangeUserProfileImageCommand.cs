using Application.Dtos.Identity;

namespace Application.Commands.Identity.ChangeProfilePhoto;

public record ChangeUserProfileImageCommand(ChangeImagePhotoDto ImageDto) : ICommand;