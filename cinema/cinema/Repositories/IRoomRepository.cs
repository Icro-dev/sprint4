using cinema.Models;

namespace cinema.Repositories;

public interface IRoomRepository
{
    public Room findRoomByShow(Show show);

    public Task<List<Room>> ListOfAllRooms();

    public Task<Room?> FindRoomById(int id);

    public void Add(Room room);

    public void SaveRoom();

    public void UpdateRoom(Room room);

    public void RemoveRoom(Room room);

    public bool RoomExists(int id);
}