namespace BloodBankAPI.Services
{
    public class DonorHelper
    {
        //check donor availability based on last donation date
        public static bool CanDonate(DateTime? lastDonationDate)
        {
            if (lastDonationDate == null) return true;
            return DateTime.UtcNow.Date >= lastDonationDate.Value.Date.AddDays(90);
        }

        //calculate next available donation date
        public static DateTime? NextAvailableDate(DateTime? lastDonaionDate)
        {
            if (lastDonaionDate == null) return DateTime.UtcNow;
            return lastDonaionDate?.AddDays(90);
        }




    }
}
