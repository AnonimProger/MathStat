using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;

namespace WinFormsApp1
{
    internal class MathHelper
    {
        public static int VARIANT = 17;


        public string Path { get;} = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + @$"\Data\11-110\{VARIANT}\r1z1.csv";
        public async void WriteAns(System.Windows.Forms.Label label)
        {
            
            List<decimal> dataL = new List<decimal>();

            foreach (var line in File.ReadAllLines(Path))
            {
                try
                {
                    var text = line.Replace(".", ",");
                    var number = Convert.ToDecimal(text);
                    dataL.Add(number);

                }
                catch (Exception)
                {
                    Console.WriteLine(line + " is not number");
                }
            }
            var data = dataL.ToArray();
            Array.Sort(data);


            /*            Console.WriteLine($"{GetChoiceVolume(data)}: объём выборки");
                        Console.WriteLine($"{GetMinChoice(data)}: минимум");
                        Console.WriteLine($"{GetMaxChoice(data)}: максимум");
                        Console.WriteLine($"{GetMaxChoice(data) - GetMinChoice(data)}: размах");
                        decimal middle = Math.Round(GetChoiceVolume(data) / data.Length, 2);
                        Console.WriteLine($"{middle}: среднее");
                        var dispersion = GetDispersionShifted(data, middle);
                        Console.WriteLine($"{dispersion}: дисперсия - смещенная оценка");
                        Console.WriteLine($"{GetDispersionNotShifted(data, middle)}: дисперсия - несмещенная оценка");
                        var deviation = GetDeviation((float)dispersion);
                        Console.WriteLine($"{deviation}: стандартное отклонение");
                        Console.WriteLine($"{GetCoefAsim(deviation, (float)middle, n, data)}: коэффициент асимметрии");
                        Console.WriteLine($"{GetMedian(data)}: медиана");
                        Console.WriteLine($"{GetInterquartileRange(data)}: интерквартильная широта");*/


            label.Text = "";
            label.Text += $"{data.Length}: объём выборки \n" +
            $"{GetMinChoice(data)}: минимум \n" +
            $"{GetMaxChoice(data)}: максимум \n" +
            $"{GetMaxChoice(data) - GetMinChoice(data)}: размах \n";
            decimal middle = Math.Round(GetChoiceVolume(data) / data.Length, 2);
            label.Text += $"{middle}: среднее \n";
            var dispersion = GetDispersionShifted(data, middle);
            label.Text += $"{dispersion}: дисперсия - смещенная оценка \n" +
            $"{GetDispersionNotShifted(data, middle)}: дисперсия - несмещенная оценка \n";
            var deviation = GetDeviation((float)dispersion);
            label.Text += $"{deviation}: стандартное отклонение \n" +
            $"{GetCoefAsim(deviation, (float)middle, data.Length, data)}: коэффициент асимметрии \n" +
            $"{GetMedian(data)}: медиана \n" +
            $"{GetInterquartileRange(data)}: интерквартильная широта \n";
        }


static decimal GetChoiceVolume(decimal[] data)
{
    decimal sum = 0;
    foreach (var el in data)
    {
        sum += el;
    }
    return sum;
}

static decimal GetMinChoice(decimal[] data)
{
    var min = data[0];
    foreach (var el in data)
    {
        if (el < min)
            min = el;
    }

    return min;
}
static decimal GetMaxChoice(decimal[] data)
{
    var max = data[0];
    foreach (var el in data)
    {
        if (el > max)
            max = el;
    }

    return max;
}

static decimal GetDispersionShifted(decimal[] data, decimal middle)
{
    decimal multiply = 0;
    foreach (var el in data)
    {
        multiply += (el - middle) * (el - middle);
    }

    return Math.Round(multiply / data.Length, 2);
}
static decimal GetDispersionNotShifted(decimal[] data, decimal middle)
{
    decimal multiply = 0;
    foreach (var el in data)
    {
        multiply += (el - middle) * (el - middle);
    }
    return Math.Round(data.Length / ((decimal)data.Length - 1) * (multiply / data.Length), 2);
}

static double GetDeviation(float dispersion)
{
    return Math.Round((float)Math.Sqrt(dispersion), 2);
}

static double GetCoefAsim(double deviation, double middle, int n, decimal[] data)
{
    double sum = 0;
    foreach (var el in data)
    {
        sum += Math.Pow((double)el - middle, 3);
    }

    return Math.Round(sum / (Math.Pow(deviation, 3) * n), 2);
}

static decimal GetMedian(decimal[] data)
{
    var n = data.Length;
    return (n % 2 == 0) switch
    {
        true => (data[n / 2 - 1] + data[n / 2]) / 2,
        false => data[n / 2]
    };
}

static decimal GetInterquartileRange(decimal[] data)
{
    //var a = data[(int)((n - 1) * .25 + 1)] + data[(int)((n - 1) * .25 + 1)] / 2;
    var n = data.Length;
    var downQuartile = ((n - 1) % 4 != 0) switch
    {
        true => (data[(int)((decimal).25 * (n - 1))] + data[(int)((decimal).25 * (n - 1) + 1)]) / 2,
        false => data[(int)(0.25 * (n - 1))]
    };
    var upperQuartile = ((n - 1) % 4 != 0) switch
    {
        true => (data[(int)((decimal).75 * (n - 1))] + data[(int)((decimal).75 * (n - 1) + 1)]) / 2,
        false => data[(int)(0.75 * (n - 1))]
    };
    return upperQuartile - downQuartile;
}
    }
}
