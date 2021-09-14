using System;

namespace HMSBLL
{
    public class UserType
    {
        public static readonly string SystemAdmin = "System Admin";
        public static readonly string Admin = "Admin";
        public static readonly string User = "User";
        public static readonly string Manager = "Manager";
        public static readonly string Operation = "Operation";
        public static readonly string Doctor = "Doctor";
        public static readonly string Maintenance = "Maintenance";
    }

    public class DateFormater
    {
        public static string GetMonthName(int monthNo)
        {
            string monthName = string.Empty;
            switch (monthNo)
            {
                case 1:
                    monthName = "January";
                    break;
                case 2:
                    monthName = "February";
                    break;
                case 3:
                    monthName = "March";
                    break;
                case 4:
                    monthName = "April";
                    break;
                case 5:
                    monthName = "May";
                    break;
                case 6:
                    monthName = "June";
                    break;
                case 7:
                    monthName = "July";
                    break;
                case 8:
                    monthName = "Augest";
                    break;
                case 9:
                    monthName = "September";
                    break;
                case 10:
                    monthName = "October";
                    break;
                case 11:
                    monthName = "November";
                    break;
                case 12:
                    monthName = "December";
                    break;
                default:
                    monthName = "";
                    break;
            }
            return monthName;
        }
    }

    public class Calculate
    {
        public static decimal InvestigationDiscountAmount(decimal actualAmount, decimal percentage)
        {
            if(percentage > 0)
            {
                var netPercentage = (percentage / 100);
                var amount = (actualAmount * netPercentage);
                return Math.Round(amount,2);
            }
            else
            {
                return 0;
            }
        }
    }

}
