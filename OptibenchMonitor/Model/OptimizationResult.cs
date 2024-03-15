
namespace Model
{
    public class OptimizationResult
    {
        public required int Id { get; set; }
        public required double[] X { get; set; } 
        public required double Y { get; set; } 
        public required string Params { get; set; } 
        public required string ProblemName { get; set; } 
        public required string EvaluationCount { get; set; } 
        public OptimizationResult(double[] x, double y, string parameters, string problemName, string evaluationCount)
        {
            X = x;
            Y = y;
            Params = parameters; 
            ProblemName = problemName;
            EvaluationCount = evaluationCount;
        }
        public OptimizationResult(){}
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