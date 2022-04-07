using cinema.Data;
using cinema.Models;

namespace cinema.Repositories;

public class SubscriberRepository : ISubscriberRepository
{

    private readonly CinemaContext _context;

    public SubscriberRepository(CinemaContext context)
    {
        _context = context;
    }
    
    public void addSubscriber(Subscriber subscriber)
    {
        _context.Add(subscriber);
    }

    public void saveSubscriber()
    {
        _context.SaveChanges();
    }
}