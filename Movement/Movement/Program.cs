using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Movement
{
    class Move
    {
        private double xSpd;
        private double ySpd;
        private double angle;
        private const double g = 9.8;
        private List<List<double>> cXYT = new List<List<double>>();

        public Move(double s, double a)
        {
            this.angle = Math.PI * a / 180.0;
            xSpd = s * Math.Cos(angle);
            ySpd = s * Math.Sin(angle);
        }
        public void InitList()
        {
            this.cXYT.Add(new List<double>());
            this.cXYT.Add(new List<double>());
            this.cXYT.Add(new List<double>());
        }
        public void Calculate(double s)
        {
            StreamWriter f = new StreamWriter("test123.txt");
            double flt = 2 * ySpd / g;
            for (double i = 1; i < flt; i += s)
            {
                InitList();
                cXYT[0].Add(xSpd * i);
                cXYT[1].Add(ySpd * i - (g * i * i / 2));
                cXYT[2].Add(i);
                for (int j = 0; j < 3; j++)
                {
                    f.WriteLine(cXYT[j].Last());
                    Console.WriteLine(cXYT[j].Last());
                }
                if ((flt - i) < s)
                {
                    cXYT[0].Add(xSpd * flt);
                    cXYT[1].Add(0);
                    cXYT[2].Add(flt);
                    for (int j = 0; j < 3; j++)
                    {
                        f.WriteLine(cXYT[j].Last());
                    }
                    f.Close();
                }
            }
            Console.WriteLine("Полёт окончен по плану. Время полёта: {0} секунд. Дистанция: {1} метров", flt, cXYT[0].Last());
            f.Close();
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            double sp, ang, step;
            sp = Convert.ToDouble(Console.ReadLine());
            ang = Convert.ToDouble(Console.ReadLine());
            step = Convert.ToDouble(Console.ReadLine());
            Move n1 = new Move(sp, ang);
            n1.Calculate(step);
        }
    }
}
