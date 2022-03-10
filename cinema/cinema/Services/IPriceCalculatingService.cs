namespace cinema.Services;

public interface IPriceCalculatingService
{
    public double pricePerTicket(int showId);
    public double totalPrice(int quantity, int showId);
}