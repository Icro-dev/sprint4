namespace cinema.Services;

public interface IPaymentAdapter
{
    public bool verifyPayment(double amount);
}