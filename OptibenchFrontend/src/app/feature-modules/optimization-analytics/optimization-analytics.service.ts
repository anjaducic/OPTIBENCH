import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { OptimizationResult } from "../model/optimization-result.model";
import { environment } from "src/env/environment";

@Injectable({
    providedIn: "root",
})
export class OptimizationAnalyticsService {
    constructor(private http: HttpClient) {}

    getResults(): Observable<OptimizationResult[]> {
        return this.http.get<OptimizationResult[]>(
            environment.apiHost + "results",
        );
    }

    getResultsByProblemAndOptimizer(
        problem: string,
        optimizer: string,
    ): Observable<OptimizationResult[]> {
        return this.http.get<OptimizationResult[]>(
            environment.apiHost +
                "results/problemName/" +
                problem +
                "/optimizerName/" +
                optimizer,
        );
    }
}
