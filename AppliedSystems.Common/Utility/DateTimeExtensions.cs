using System;

namespace AppliedSystems.Common
{
    public static class DateTimeExtensions
    {
        public static int CalculateAgeOnDate(this DateTime dateOfBirth, DateTime date)
        {
            var years = date.Year - dateOfBirth.Year;

            if ((dateOfBirth.Month > date.Month) || (dateOfBirth.Month == date.Month && dateOfBirth.Day > date.Day))
            {
                years--;
            }

            return years;
        }
    }
}
