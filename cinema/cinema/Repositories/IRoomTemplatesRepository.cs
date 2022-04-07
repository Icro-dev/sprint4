using cinema.Models;

namespace cinema.Repositories;

public interface IRoomTemplatesRepository
{
    public Task<List<RoomTemplate>> ListOfAllRoomTemplates();
    
    public Task<RoomTemplate> FindRoomTemplatesById(int id);
    
    public void Add(RoomTemplate roomTemplate);

    public void SaveRoomTemplate();

    public void UpdateRoomTemplate(RoomTemplate roomTemplate);

    public void RemoveRoomTemplate(RoomTemplate roomTemplate);

    public bool RoomTemplateExists(int id);
}
