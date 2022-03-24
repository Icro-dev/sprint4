using cinema.Models;

namespace cinema.Services
{
    public interface IRoomService
    {
        public Room GetShowRoom(Show show);
        public int[] GetRoomTemplate(Room room);
    }
}
