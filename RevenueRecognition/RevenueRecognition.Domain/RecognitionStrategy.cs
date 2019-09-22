namespace RevenueRecognition.Domain
{
    public abstract class RecognitionStrategy
    {
        public abstract void CalculateRevenueRecognitions(Contract contract);
    }
}
