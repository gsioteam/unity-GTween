
using System;

namespace GTween
{
    public class Easing
    {
        const double BackConstant = 1.70158;
        const double BackConstant2 = BackConstant * 1.525;

        public static double Linear(double amount) => amount;
        public static double QuadraticIn(double amount) => amount * amount;
        public static double QuadraticOut(double amount) => amount * (2 - amount);
        public static double QuadraticInOut(double amount) => ((amount *= 2) < 1) ? 0.5 * amount * amount : -0.5 * (--amount * (amount - 2) - 1);
        public static double CubicIn(double amount) => amount * amount * amount;
        public static double CubicOut(double amount) => --amount * amount * amount + 1;
        public static double CubicInOut(double amount) => ((amount *= 2) < 1) ? 0.5 * amount * amount * amount : 0.5 * ((amount -= 2) * amount * amount + 2);
        public static double QuarticIn(double amount) => amount * amount * amount * amount;
        public static double QuarticOut(double amount) => 1 - --amount * amount * amount * amount;
        public static double QuarticInOut(double amount) => ((amount *= 2) < 1) ? 0.5 * amount * amount * amount * amount : -0.5 * ((amount -= 2) * amount * amount * amount - 2);
        public static double QuinticIn(double amount) => amount * amount * amount * amount * amount;
        public static double QuinticOut(double amount) => --amount * amount * amount * amount * amount + 1;
        public static double QuinticInOut(double amount) => ((amount *= 2) < 1) ? 0.5 * amount * amount * amount * amount * amount : 0.5 * ((amount -= 2) * amount * amount * amount * amount + 2);
        public static double SinusoidalIn(double amount) => 1 - Math.Sin(((1.0 - amount) * Math.PI) / 2);
        public static double SinusoidalOut(double amount) => Math.Sin((amount * Math.PI) / 2);
        public static double SinusoidalInOut(double amount) => 0.5 * (1 - Math.Sin(Math.PI * (0.5 - amount)));
        public static double ExponentialIn(double amount) => amount == 0 ? 0 : Math.Pow(1024, amount - 1);
        public static double ExponentialOut(double amount) => amount == 1 ? 1 : 1 - Math.Pow(2, -10 * amount);
        public static double ExponentialInOut(double amount)
        {
            if (amount == 0)
            {
                return 0;

            }

            if (amount == 1)
            {
                return 1;

            }

            if ((amount *= 2) < 1)
            {
                return 0.5 * Math.Pow(1024, amount - 1);

            }

            return 0.5 * (-Math.Pow(2, -10 * (amount - 1)) + 2);
        }
        public static double CircularIn(double amount) => 1 - Math.Sqrt(1 - amount * amount);
        public static double CircularOut(double amount) => Math.Sqrt(1 - --amount * amount);
        public static double CircularInOut(double amount) => ((amount *= 2) < 1) ? -0.5 * (Math.Sqrt(1 - amount * amount) - 1) : 0.5 * (Math.Sqrt(1 - (amount -= 2) * amount) + 1);
        public static double ElasticIn(double amount) => (amount == 0) ? 0 : ((amount == 1) ? 1 : -Math.Pow(2, 10 * (amount - 1)) * Math.Sin((amount - 1.1) * 5 * Math.PI));
        public static double ElasticOut(double amount) => (amount == 0) ? 0 : ((amount == 1) ? 1 : Math.Pow(2, -10 * amount) * Math.Sin((amount - 0.1) * 5 * Math.PI) + 1);
        public static double ElasticInOut(double amount)
        {
            if (amount == 0)
            {
                return 0;
            }

            if (amount == 1)
            {
                return 1;

            }

            amount *= 2;


            if (amount < 1)
            {
                return -0.5 * Math.Pow(2, 10 * (amount - 1)) * Math.Sin((amount - 1.1) * 5 * Math.PI);

            }

            return 0.5 * Math.Pow(2, -10 * (amount - 1)) * Math.Sin((amount - 1.1) * 5 * Math.PI) + 1;
        }
        public static double BackIn(double amount) => amount == 1 ? 1 : amount * amount * ((BackConstant + 1) * amount - BackConstant);
        public static double BackOut(double amount) => amount == 0 ? 0 : --amount * amount * ((BackConstant + 1) * amount + BackConstant) + 1;
        public static double BackInOut(double amount) => ((amount *= 2) < 1) ? 0.5 * (amount * amount * ((BackConstant2 + 1) * amount - BackConstant2)) : 0.5 * ((amount -= 2) * amount * ((BackConstant2 + 1) * amount + BackConstant2) + 2);
        public static double BounceIn(double amount) => 1 - BounceOut(1 - amount);
        public static double BounceOut(double amount)
        {
            if (amount < 1 / 2.75)
            {
                return 7.5625 * amount * amount;

            }
            else if (amount < 2 / 2.75)
            {
                return 7.5625 * (amount -= 1.5 / 2.75) * amount + 0.75;

            }
            else if (amount < 2.5 / 2.75)
            {
                return 7.5625 * (amount -= 2.25 / 2.75) * amount + 0.9375;

            }
            else
            {
                return 7.5625 * (amount -= 2.625 / 2.75) * amount + 0.984375;

            }
        }
        public static double BounceInOut(double amount) => (amount < 0.5) ? BounceIn(amount * 2) * 0.5 : BounceOut(amount * 2 - 1) * 0.5 + 0.5;
        
    }
}
