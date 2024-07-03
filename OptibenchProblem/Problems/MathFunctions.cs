using System;
using Microsoft.OpenApi.Any;

// klasa koja sadrzi samo funkcije probleme
public static class MathFunctions
{
    
    public static double Sphere(double[] x)
    {
        return x.Select(xi => xi * xi).Sum();
    }

    
    public static double Rosenbrock(double[] x)
    {
      double sum = 0;
        for (int i = 0; i < x.Length - 1; i++)
        {
            double term1 = x[i + 1] - x[i] * x[i];
            double term2 = x[i] - 1;
            sum += 100 * term1 * term1 + term2 * term2;
        }
        return sum;
    }

    public static double Rastrigin(double[] x)
    {
        double sum = 0;
        double A = 10;
        int n = x.Length;

        foreach (double xi in x)
        {
            sum += Math.Pow(xi, 2) - A * Math.Cos(2 * Math.PI * xi);
        }

        return A * n + sum;
    }

    public static double Shekel(double[] x)
    {
        if(x.Length > 4) 
            return double.NaN;
        int m = 5;
        double sum = 0;

        double[] c = [0.1,0.2,0.2,0.4,0.4];

        double[,] a = {
            { 2, 2, 2, 2 },
            { 4, 4, 4, 4 },
            { 8, 8, 8, 8 },
            { 6, 6, 6, 6 },
            { 3, 7, 3, 7 }
        };

        for (int i = 0; i < m; i++)
        {
            double innerSum = 0;
            for (int j = 0; j < x.Length; j++)
            {
                innerSum += Math.Pow(x[j] - a[i, j], 2);
            }
            sum += 1.0 / (c[i] + innerSum);
        }

        return sum;
    }

    public static double Matyas(double[] x)
    {
        if(x.Length != 2)
            return double.NaN;
        double term1 = 0.26 * (Math.Pow(x[0], 2) + Math.Pow(x[1], 2));
        double term2 = -0.48 * x[0] * x[1];
        return term1 + term2;
    }

    public static double Beale(double[] x)
    {
        if(x.Length != 2)
            return double.NaN;

        double x1 = x[0];
        double x2 = x[1];
        double term1 = Math.Pow(1.5 - x1 + x1 * x2, 2);
        double term2 = Math.Pow(2.25 - x1 + x1 * Math.Pow(x2, 2), 2);
        double term3 = Math.Pow(2.625 - x1 + x1 * Math.Pow(x2, 3), 2);

        return term1 + term2 + term3;
    }

    public static double Booth(double[] x)
    {
        if(x.Length != 2)
            return double.NaN;

        double x1 = x[0];
        double x2 = x[1];
        double term1 = Math.Pow(x1 + 2 * x2 - 7, 2);
        double term2 = Math.Pow(2 * x1 + x2 - 5, 2);

        return term1 + term2;
    } 



    //sa ogranicenjima:
    public static double GomezLevi(double[] x)
    {
        if (!GomezLeviConstraints(x))
        {
            return double.NaN; // nije ispunjen uslov
        }

        double f = 4 * Math.Pow(x[0], 2) - 2.1 * Math.Pow(x[0], 4) + (1.0 / 3.0) * Math.Pow(x[0], 6)
                + x[0] * x[1] - 4 * Math.Pow(x[1], 2) + 4 * Math.Pow(x[1], 4);
        return f;
    }

    private static bool GomezLeviConstraints(double[] x)
    {
        double constraint = -Math.Sin(4 * Math.PI * x[0]) + 2 * Math.Pow(Math.Sin(2 * Math.PI * x[1]), 2) - 1.5;
        return constraint <= 0; // ispunjen->true
    }

    public static double MishrasBird(double[] x)
    {
        if (!MishrasBirdConstraints(x))
        {
            return double.NaN; // nije ispunjen uslov
        }

        double sinY = Math.Sin(x[1]);
        double cosX = Math.Cos(x[0]);
        double exp1 = Math.Exp(Math.Pow(1 - cosX, 2));
        double exp2 = Math.Exp(Math.Pow(1 - sinY, 2));
        double f = sinY * exp1 + cosX * exp2 + Math.Pow(x[0] - x[1], 2);
        

        return f;
    }

    private static bool MishrasBirdConstraints(double[] x)
    {
        double constraint = Math.Pow(x[0] + 5, 2) + Math.Pow(x[1] + 5, 2) - 25;
        return constraint < 0; // ispunjen->true
    }






}
