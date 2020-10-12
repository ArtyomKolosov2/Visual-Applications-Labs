using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Lab_Work_9.ViewModels.RadioButtonViewModel;
using static System.Math;

namespace Lab_Work_9.Models
{
    public static class MathFunctions
    {
        public static async Task<string> StartCountAsync(double xn, double xk, int n, int m, MathFunctionHandler mathFunction)
        {
            return await Task.Run(() => StartCount(xn, xk, n, m, mathFunction));
        }
        public static string StartCount(double xn, double xk, int n, int m, MathFunctionHandler mathFunction)
        {
            double h = (xk - xn) / m;
            double fx = 0;
            if (mathFunction == null)
            {
                return "Error: math function isn't choosen!";
            }
            StringBuilder result_str = new StringBuilder();
            for (int i = 1; xn < xk && i < m+1; i++, xn += h)
            {
                fx = Round(mathFunction(xn), 4);
                result_str.Append
                (
                    $"{i.ToString()}. f(x) = {fx}, S(x) = {GetSXFunctionResult(fx, n)}  " +
                    $"Y(x) = {GetYXFunctionResult(fx)}\n"
                );
            }
            return result_str.ToString();
        }

        public static double GetSXFunctionResult(double x, int n)
        {
            double sum = 0;
            int k = 0;
            while (k < n)
            {
                sum += CountSXElement(x, k);
                k++;
            }
            return sum;
        }
        public static double CountSXElement(double x, int k)
        {
            double result = Pow(-1, k) * ((2d * Pow(k, 2) + 1) / Factorial(2 * k)) * Pow(x, 2 * k);
            return result;
        }
        public static long Factorial(int num)
        {
            long result = 1;
            for (int i = 2; i <= num; i++)
            {
                result *= i;
            }
            return result;
        }
        public static double GetYXFunctionResult(double x)
        {
            return (1 - ((x * x) / 2d)) * Cos(x) - (x / 2d) * Sin(x);
        }
    }
}
