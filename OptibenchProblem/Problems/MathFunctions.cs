using System;

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
        if(x.Length > 4)  //??? da li smije tako, i da li je to ok ako je <4 zbog parametara (a,c) 
            return double.NaN;
        int m = 5;
        double sum = 0;

        double[] c = [0.1,0.2,0.2,0.4,0.4]; //provjeriti sve parametre

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


}
