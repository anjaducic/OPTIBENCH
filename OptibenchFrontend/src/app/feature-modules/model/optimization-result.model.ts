export interface OptimizationResult {
    id: number;
    x: number[];
    y: number;
    params: JSON;
    problemInfo: string;
    evaluationCount: JSON;
    optimizerName: string;
}
