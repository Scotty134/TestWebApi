namespace TestWebApiIdentity.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalcuateAge(this DateTime dob)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var dobDt = DateOnly.FromDateTime(dob);

            var age = today.Year - dob.Year;

            if (dobDt > today.AddYears(-age)) age--;

            return age;
        }
    }
}