using interfaces;
using Utilities;

namespace Implementations
{
    public class PsoOptimizer : IOptimizer
    {
        

        public Task<(double[], double, int)> Optimize(IProblem problem)
        {
            throw new NotImplementedException();
        }
    }

    public class PSOOptions
    {
        public int NPart { get; set; } = 30;
        public int NIter { get; set; } = 100;
        public double Cbi { get; set; } = 2.5;
        public double Cbf { get; set; } = 0.5;
        public double Cgi { get; set; } = 0.5;
        public double Cgf { get; set; } = 2.5;
        public double Wi { get; set; } = 0.9;
        public double Wf { get; set; } = 0.4;
        public double VMax { get; set; } = double.PositiveInfinity;
        public double VMaxScale { get; set; } = double.NaN;
        public double VSpanInit { get; set; } = 1;
        public double?[,]? InitPopulation { get; set; } = null;
        public double InitOffset { get; set; } = 0;
        public double InitSpan { get; set; } = 1;
        public double TrustOffset { get; set; } = 0;
    }


    public class Particle
    {
        private readonly int _numDimensions;
        private readonly PSOOptions _options;

        public double[] Position { get; private set; }
        public double[] Velocity { get; private set; }
        public double[] BestPosition { get; private set; }
        public double BestFitness { get; private set; } = -1;
        public double Fitness { get; private set; } = -1;

        public Particle(double[] initialPosition, int numDimensions, PSOOptions options)
        {
            _numDimensions = numDimensions;
            _options = options;

            Position = initialPosition; //u py malo drugacije, provjeriti?
            Velocity = [];
            BestPosition = [];

            // inic brzina
            var random = new Random();
            for (var i = 0; i < numDimensions; i++)
            {
                Velocity[i] = (random.NextDouble() - 0.5) * 2 * options.VSpanInit;
            }
        }

        public async Task Evaluate(IProblem problem)
        {
            Fitness = await problem.GetValue(Position);

            // azuriraj
            if (Fitness < BestFitness || BestFitness == -1)
            {
                BestPosition = (double[])Position.Clone();
                BestFitness = Fitness;
            }
        }

        public void UpdateVelocity(double[] globalBestPosition, int maxIterations, int currentIteration)
        {
            // sracunaj PSO parametre
            var w = LinearInterpolation(_options.Wf, _options.Wi, maxIterations, 0, currentIteration);
            var cp = LinearInterpolation(_options.Cbf, _options.Cbi, maxIterations, 0, currentIteration);
            var cg = LinearInterpolation(_options.Cgf, _options.Cgi, maxIterations, 0, currentIteration);

            var random = new Random();
            for (var i = 0; i < _numDimensions; i++)
            {
                var r1 = random.NextDouble();
                var r2 = random.NextDouble();
                var velCognitive = cp * r1 * (BestPosition[i] - Position[i]);
                var velSocial = cg * r2 * (globalBestPosition[i] - Position[i]);
                Velocity[i] = w * Velocity[i] + velCognitive + velSocial;
            }
        }

        public void UpdatePosition()
        {
            for (var i = 0; i < _numDimensions; i++)
            {
                Position[i] += Velocity[i];

                // Adjust position if necessary - ?
                //if (Position[i] > bounds[i][1])
                //    Position[i] = bounds[i][1];
                //if (Position[i] < bounds[i][0])
                //    Position[i] = bounds[i][0];
            }
        }

        private double LinearInterpolation(double xmax, double xmin, int tmax, int tmin, int t)
        {
            return xmin + ((xmax - xmin) / (tmax - tmin)) * (tmax - t);
        }
    }

    public class PSOOptimizer
    {
        public int NumDimensions { get; set; }
        public int MaxIterations { get; set; }
        public int NumParticles { get; set; }
        public Particle[] Population { get; set; }
        public PSOOptions Options { get; set; }

        public PSOOptimizer(int numDimensions, PSOOptions options)
        {
            NumDimensions = numDimensions;
            MaxIterations = options.NIter;
            NumParticles = options.NPart;
            Options = options;
            Population = [];
        }
        public async Task<(double[], double, int)> Optimize(IProblem problem)
        {
            

            if (!Options.InitPopulation!.Cast<double>().Any(double.IsNaN))
            {
                var b = Options.InitPopulation!.GetLength(0);
                var pdim = 1;
                var pno = b;

                if (pno != Options.NPart || pdim != Options.NPart)  //pisalo nvar??
                {
                    throw new Exception("The format of initial population is inconsistent with desired population");
                }

                Population = [];
                for (var i = 0; i < NumParticles; i++)
                {
                    var initialPosition = new double[NumDimensions];
                    for (var j = 0; j < NumDimensions; j++)
                    {
                        initialPosition[j] = (double)Options.InitPopulation[i, j]!;
                    }
                    Population.Append(new Particle(initialPosition, NumDimensions, Options));
                }
            }
            else
            {
                Population = [];
                for (var i = 0; i < NumParticles; i++)
                {
                    var initialPosition = new double[NumDimensions];
                    var random = new Random();
                    for (var j = 0; j < NumDimensions; j++)
                    {
                        initialPosition[j] = (random.NextDouble() - 0.5) * 2 * Options.InitSpan + Options.InitOffset;
                    }
                    Population.Append(new Particle(initialPosition, NumDimensions, Options));
                }
            }


            

            var globalBestPosition = new double[NumDimensions];
            var globalBestFitness = -1.0;

            // opt petlja
            for (var iter = 0; iter < MaxIterations; iter++)
            {
                // Evaluate fitness - za svaku cesticu
                foreach (var particle in Population)
                {
                    await particle.Evaluate(problem);   //treba li i Wait?

                    // Update global best
                    if (particle.BestFitness < globalBestFitness || globalBestFitness == -1)
                    {
                        globalBestPosition = (double[])particle.BestPosition.Clone();
                        globalBestFitness = particle.BestFitness;
                    }
                }

                // Za svaku cesticu azuriraj velocity i position
                foreach (var particle in Population)
                {
                    particle.UpdateVelocity(globalBestPosition, MaxIterations, iter);
                    particle.UpdatePosition();
                }
            }

            return (globalBestPosition, globalBestFitness, MaxIterations);
        }
    }


}
