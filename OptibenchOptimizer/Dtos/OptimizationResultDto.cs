
namespace Dtos
{
    public class OptimizationResultDto
    {
        //public int Id { get; set; }
        public double[] X { get; set; } 
        public double Y { get; set; } 
        public string Params { get; set; } 
        public string ProblemInfo { get; set; } 
        public string EvaluationCount { get; set; } 
        public string OptimizerName { get; set; }
        public OptimizationResultDto(double[] x, double y, string parameters, string problemInfo, string evaluationCount, string optimizerName)
        {
            X = x;
            Y = y;
            Params = parameters; 
            ProblemInfo = problemInfo;
            EvaluationCount = evaluationCount;
            OptimizerName = optimizerName;
        }
    }
}

/*
INSERT INTO public."Results"(
	"Id", "X", "Y", "Params", "ProblemName", "EvaluationCount")
	VALUES (1, array[1, 2, 3], 3,'params123', 'spherical', '9');

INSERT INTO public."Results" ("Id", "X", "Y", "Params", "ProblemName", "EvaluationCount")
VALUES (1, array[2, 2, 3], 3, '{"prviParam": "12.0", "drugiParam": "13.2"}', 'spherical', 
	   '{"brojac": "12.0"}');

*/