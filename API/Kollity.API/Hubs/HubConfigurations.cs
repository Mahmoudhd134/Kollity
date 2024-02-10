using Kollity.API.Hubs.Room;

namespace Kollity.API.Hubs;

public static class HubConfigurations
{
    public static readonly string BaseHubPath = "hub";

    public static void MapHubs(this WebApplication app)
    {
        app.MapHub<RoomHub>($"{BaseHubPath}/room");
    }
}