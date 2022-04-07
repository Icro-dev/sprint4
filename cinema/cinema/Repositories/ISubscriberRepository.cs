using cinema.Models;

namespace cinema.Repositories;

public interface ISubscriberRepository
{
    public void addSubscriber(Subscriber subscriber);

    public void saveSubscriber();
}