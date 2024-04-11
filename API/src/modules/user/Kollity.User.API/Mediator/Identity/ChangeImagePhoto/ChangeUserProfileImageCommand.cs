using Kollity.User.API.Abstraction.Messages;
using Kollity.User.API.Dtos.Identity;

namespace Kollity.User.API.Mediator.Identity.ChangeImagePhoto;

public record ChangeUserProfileImageCommand(ChangeImagePhotoDto ImageDto) : ICommand;