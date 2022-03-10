namespace cinema.Services;

public interface IPriceCalculatingService
{
    public double pricePerTicket();
    public double totalPrice(int quantity);
}