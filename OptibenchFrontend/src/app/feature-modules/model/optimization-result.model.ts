export interface OptimizationResult {
    id: number;
    x: number[];
    y: number;
    params: string;
    problemInfo: string;
    evaluationCount: string;
    optimizerName: string;
    paramsHashCode: number;
    exactSolution: number;
}
