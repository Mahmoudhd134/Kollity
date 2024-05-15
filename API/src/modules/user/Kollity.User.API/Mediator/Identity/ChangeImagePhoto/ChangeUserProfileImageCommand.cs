using Kollity.Common.Abstractions.Messages;
using Kollity.User.API.Dtos.Identity;

namespace Kollity.User.API.Mediator.Identity.ChangeImagePhoto;

public record ChangeUserProfileImageCommand(ChangeImagePhotoDto ImageDto) : ICommand<string>;