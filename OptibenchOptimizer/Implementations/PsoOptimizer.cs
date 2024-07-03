using interfaces;
using Utilities;



namespace Implementations
{
    
    public class PSOOptions
    {
        public double Cbi { get; set; }
        public double Cbf { get; set; }
        public double Cgi { get; set; }
        public double Cgf { get; set; }
        public double Wi { get; set; }
        public double Wf { get; set; }
        public double VSpanInit { get; set; }
        public double InitOffset { get; set; }
        public double InitSpan { get; set; }

        
    }


    public class Particle
    {
        
        private readonly int Dimensions;
        private readonly PSOOptions Options;

        public double[] Position { get; private set; }
        public double[] Velocity { get; private set; }
        public double[] BestPosition { get; private set; }
        public double BestFitness { get; private set; } = double.NaN;
        public double Fitness { get; private set; } = double.NaN;

        public Particle(double[] initialPosition, int dimension, PSOOptions options)
        {
            Dimensions = dimension;
            Options = options;

            Position = initialPosition; 
            Velocity = new double[dimension];;
            BestPosition = new double[dimension];;

          
            var random = new Random();
            for (var i = 0; i < dimension; i++)
            {
                Velocity[i] = (random.NextDouble() - 0.5) * 2 * options.VSpanInit;
            }
        }

        public async Task Evaluate(IProblem problem)
        {
            //dodajem provjeru ako npr ne zadovoljava ogranicenje (NaN), da se izignorise taj fitness
            double newFitness = await problem.GetValue(Position);
            if(double.IsNaN(newFitness))
                return;

            PSOOptimizer.iterNum++ ;
            Fitness = newFitness;
            
            // azuriraj
            if (Fitness < BestFitness || double.IsNaN(BestFitness)) //BestFitness je NaN samo na pocetku, inace ga sprijecim uslovom gore
            {
                BestPosition = (double[])Position.Clone();
                BestFitness = Fitness;
            }
        }

        public void UpdateVelocity(double[] globalBestPosition, int maxIterations, int currentIteration)
        {
            // sracunaj PSO parametre
            var w = LinearInterpolation(Options.Wf, Options.Wi, maxIterations, 0, currentIteration);
            var cp = LinearInterpolation(Options.Cbf, Options.Cbi, maxIterations, 0, currentIteration);
            var cg = LinearInterpolation(Options.Cgf, Options.Cgi, maxIterations, 0, currentIteration);

            var random = new Random();
            for (var i = 0; i < Dimensions; i++)
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
            for (var i = 0; i < Dimensions; i++)
            {
                Position[i] += Velocity[i];
            }
        }

        private double LinearInterpolation(double xmax, double xmin, int tmax, int tmin, int t)
        {
            return xmin + (xmax - xmin) / (tmax - tmin) * (tmax - t);
        }
    }

    public class PSOOptimizer : IOptimizer
    {
        public static int iterNum = 0;
        public string OptimizerName { get; } = "PSO-Csharp-DOTNET";
        public int Dimension { get; set; }
        public int MaxIterations { get; set; }
        public int NumParticles { get; set; }
        
        public List<Particle> Population { get; set; } = [];
        public PSOOptions Options { get; set; } = new();

        public PSOOptimizer(OptimizerArguments args)
        {
            Dimension = args.IntSpecs!["Dimension"];
            MaxIterations = args.IntSpecs!["MaxIterations"];
            NumParticles = args.IntSpecs!["NumParticles"];
            Options.Cbi = args.DoubleSpecs!["Cbi"];
            Options.Cbf = args.DoubleSpecs!["Cbf"];
            Options.Cgi = args.DoubleSpecs!["Cgi"];
            Options.Cgf = args.DoubleSpecs!["Cgf"];
            Options.Wi = args.DoubleSpecs!["Wi"];
            Options.Wf = args.DoubleSpecs!["Wf"];
            Options.VSpanInit = args.DoubleSpecs!["VSpanInit"];
            Options.InitOffset = args.DoubleSpecs!["InitOffset"];
            Options.InitSpan = args.DoubleSpecs!["InitSpan"];
        }
        public async Task<(double[], double, int)> Optimize(IProblem problem)
        {
            
            Population = new List<Particle>();
            for (var i = 0; i < NumParticles; i++)
            {
                var initialPosition = new double[Dimension];
                var random = new Random();
                for (var j = 0; j < Dimension; j++)
                {
                    initialPosition[j] = (random.NextDouble() - 0.5) * 2 * Options.InitSpan + Options.InitOffset;
                }
                Population.Add(new Particle(initialPosition, Dimension, Options));
            }
            
          

            var globalBestPosition = new double[Dimension];
            var globalBestFitness = double.NaN;

            
            // opt petlja
            for (var iter = 0; iter < MaxIterations; iter++)
            {
                
                // Evaluate fitness - za svaku cesticu
                (globalBestPosition, globalBestFitness) = await EvaluateFitness(problem, globalBestPosition, globalBestFitness);
                
                
                // Za svaku cesticu azuriraj velocity i position
                foreach (var particle in Population)
                {
                    particle.UpdateVelocity(globalBestPosition, MaxIterations, iter);
                    particle.UpdatePosition();
                }
            }

            return (globalBestPosition, globalBestFitness, iterNum);
        }
        
        private async Task<(double[], double)> EvaluateFitness(IProblem problem, double[] globalBestPosition, double globalBestFitness) {

             foreach (var particle in Population)
                {
                    await particle.Evaluate(problem);  
                     

                    // Update global best
                    if (particle.BestFitness < globalBestFitness || double.IsNaN(globalBestFitness))
                    {
                        globalBestPosition = (double[])particle.BestPosition.Clone();
                        globalBestFitness = particle.BestFitness;
                    }
                }
                return (globalBestPosition, globalBestFitness);
        }
    }


}


