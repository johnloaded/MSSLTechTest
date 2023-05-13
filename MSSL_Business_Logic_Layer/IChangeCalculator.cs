namespace MSSL_Business_Logic_Layer
{
    public interface IChangeCalculator
    {
        TransactionChange CalculateChange(double amount, double purchasePrice);
    }
}