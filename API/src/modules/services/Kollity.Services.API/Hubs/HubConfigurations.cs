using Kollity.Services.API.Hubs.Hubs.Room;

namespace Kollity.Services.API.Hubs;

public static class HubConfigurations
{
    public const string BaseHubPath = "hub";

    public static void MapServicesHubs(this WebApplication app)
    {
        app.MapHub<RoomHub>($"{BaseHubPath}/room");
    }
}