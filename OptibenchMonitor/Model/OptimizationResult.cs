
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class OptimizationResult
    {
        [Key]
        public  int Id { get; set; }
        public required double[] X { get; set; } 
        public required double Y { get; set; } 

        public required string Params { get; set; } 
        public required string ProblemInfo { get; set; } 
        public required string EvaluationCount { get; set; } 
        public required string OptimizerName { get; set; }

        public int ParamsHashCode { get; set; } //ne treba required zbog baze
        public OptimizationResult(double[] x, double y, string parameters, string problemInfo, string evaluationCount, string optimizerName)
        {
            X = x;
            Y = y;
            Params = parameters; 
            ProblemInfo = problemInfo;
            EvaluationCount = evaluationCount;
            OptimizerName = optimizerName;
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