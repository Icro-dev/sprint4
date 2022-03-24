namespace cinema.Services;

public interface IPriceCalculatingService
{
    public double pricePerTicket(int showId);
    public double ticketCost(int quantity, int showId);
    
    public double Discount(int ChildDiscount, int StudentDiscount, int SeniorDiscount);

    public double Premium(int Popcorn);

    public double ArrangementCost(Arrangements arrangement);

    public double OrderCost(double Discount, double Premium, double SubTotalCost, double arrangementCost);

}