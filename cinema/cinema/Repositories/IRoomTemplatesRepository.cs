using cinema.Models;

namespace cinema.Repositories;

public interface IRoomTemplatesRepository
{
    public async Task<List<RoomTemplate>> ListOfAllRoomTemplates();
}