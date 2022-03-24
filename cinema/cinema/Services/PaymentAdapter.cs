namespace cinema.Services;

public class PaymentAdapter : IPaymentAdapter
{
    public bool verifyPayment(double amount)
    {
        return true;
    }
}