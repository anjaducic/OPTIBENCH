export interface OptimizationResult {
    id: number;
    x: number[];
    y: number;
    params: JSON;
    problemInfo: JSON;
    evaluationCount: JSON;
    optimizerName: string;
}
