using cinema.Models;

namespace cinema.Repositories;

public interface IRoomRepository
{
    public Room findRoomByShow(Show show);

    public Task<List<Room>> ListOfAllRooms();
}