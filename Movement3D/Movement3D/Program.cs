using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Movement
{
    class Move
    {
        private double xSpd,ySpd,zSpd;
        private double phi,psi;
        private const double g = 9.8;
        private List<List<double>> cXYZT = new List<List<double>>();

        public Move(double s, double ph, double ps)
        {
            this.phi = Math.PI * ph / 180.0;
            this.psi = Math.PI * ps / 180.0;
            zSpd = s * Math.Sin(psi);
            xSpd = s * Math.Cos(psi) * Math.Cos(phi);
            ySpd = s * Math.Cos(psi) * Math.Sin(phi);
        }
        public void InitList()
        {
            for (int i = 0; i < 4; i++)
                this.cXYZT.Add(new List<double>());
        }
        public void Calculate(double t)
        {
            StreamWriter f = new StreamWriter("test123.txt");
            for (int i = 1; i < t; i++)
            {
                InitList();
                cXYZT[0].Add(xSpd * i);
                cXYZT[1].Add(ySpd * i);
                cXYZT[2].Add(zSpd * i - (g * i * i / 2));
                cXYZT[3].Add(i);
                if (cXYZT[2].Last() < 0)
                {
                    double temp = 2 * ySpd / g;
                    cXYZT[0].Add(xSpd * temp);
                    cXYZT[1].Add(ySpd * temp);
                    cXYZT[2].Add(0);
                    cXYZT[3].Add(temp);
                    double distance = Math.Sqrt(cXYZT[0].Last() * cXYZT[0].Last() + cXYZT[1].Last() * cXYZT[1].Last());
                    Console.WriteLine("Полёт окончен. Время полёта: {0} секунд. Дистанция: {1} метров", temp, distance);
                    for (int j = 0; j < 3; j++)
                    {
                        f.WriteLine(cXYZT[j].Last());
                    }
                    f.Close();
                    break;
                }
                for (int j = 0; j < 4; j++)
                {
                    f.WriteLine(cXYZT[j].Last());
                    Console.WriteLine(cXYZT[j].Last());
                }
            }
            f.Close();
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            double sp, phi,psi, time;
            sp = Convert.ToDouble(Console.ReadLine());
            phi = Convert.ToDouble(Console.ReadLine());
            psi = Convert.ToDouble(Console.ReadLine());
            time = Convert.ToDouble(Console.ReadLine());
            Move n1 = new Move(sp, phi,psi);
            n1.Calculate(time);
        }
    }
}
