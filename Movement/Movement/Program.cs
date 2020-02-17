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
            this.angle = a;
            xSpd = s * Math.Cos(a);
            ySpd = s * Math.Sin(a);
        }
        public void initList()
        {
            this.cXYT.Add(new List<double>());
            this.cXYT.Add(new List<double>());
            this.cXYT.Add(new List<double>());
        }
        public void Calculate(double t)
        {
            StreamWriter f = new StreamWriter("test123.txt");
            for (int i = 1; i < t; i++)
            {
                initList();
                cXYT[0].Add(xSpd * i);
                cXYT[1].Add(ySpd * i - (g * i * i / 2));
                cXYT[2].Add(i);
                if (cXYT[1].Last() < 0)
                {
                    double temp = 2 * ySpd / g;
                    cXYT[0].Add(xSpd * temp);
                    cXYT[1].Add(0);
                    cXYT[1].Add(temp);
                    Console.WriteLine("Полёт окончен. Время полёта: {0} секунд. Дистанция: {1} метров", temp, cXYT[0].Last());
                    for (int j = 0; j < 3; j++)
                    {
                        f.WriteLine(cXYT[j].Last());
                    }
                    f.Close();
                    break;
                }
                for(int j = 0; j < 3; j++)
                {
                    f.WriteLine(cXYT[j].Last());
                    Console.WriteLine(cXYT[j].Last());
                }
            }
            f.Close();
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            double sp, ang,time;
            sp = Convert.ToDouble(Console.ReadLine());
            ang = Convert.ToDouble(Console.ReadLine());
            time = Convert.ToDouble(Console.ReadLine());
            Move n1 = new Move(sp, ang);
            n1.Calculate(time);
        }
    }
}
