import { Component, Inject, OnInit } from "@angular/core";
import { OptimizationResult } from "../../model/optimization-result.model";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { OptimizationAnalyticsService } from "../optimization-analytics.service";

@Component({
    selector: "app-ranking",
    templateUrl: "./ranking.component.html",
    styleUrls: ["./ranking.component.css"],
})
export class RankingComponent implements OnInit {
    groupedResults: { [key: number]: OptimizationResult[] } = {};
    rankedResults: {
        [key: string]: { averageY: number; count: number; params: string };
    } = {};
    exactSolution: number = 0;
    Object = Object;

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        private service: OptimizationAnalyticsService,
    ) {}

    ngOnInit(): void {
        this.service
            .getResultsByProblemAndOptimizer(
                this.data.problemName,
                this.data.optimizerName,
            )
            .subscribe({
                next: (results: OptimizationResult[]) => {
                    this.exactSolution = results[0].exactSolution;
                    this.groupResultsByParamsHashCode(results);
                    this.calculateRankedResults();
                },
            });
    }

    parseJsonString(jsonString: string): any {
        try {
            return JSON.parse(jsonString);
        } catch (e) {
            console.error("Error parsing JSON string:", e);
            return null;
        }
    }
    getFormatted(valueToFormat: string): string[] {
        const paramsValues = new Set<string>();
        const paramsObj = this.parseJsonString(valueToFormat);

        if (paramsObj !== null) {
            Object.entries(paramsObj).forEach(([key, value]) => {
                // ako je dictionary u dictionariju
                if (typeof value === "object" && value !== null) {
                    // tad iteriram kroz key value
                    Object.entries(value).forEach(([innerKey, innerValue]) => {
                        console.log(`${innerKey}: ${innerValue}`);
                        paramsValues.add(`${innerKey}: ${innerValue}`);
                    });
                } else {
                    // samo dictionaries
                    console.log(`${key}: ${value}`);
                    paramsValues.add(`${key}: ${value}`);
                }
            });
        }

        return Array.from(paramsValues);
    }

    groupResultsByParamsHashCode(results: OptimizationResult[]): void {
        results.forEach(result => {
            if (!this.groupedResults[result.paramsHashCode]) {
                this.groupedResults[result.paramsHashCode] = [];
            }
            this.groupedResults[result.paramsHashCode].push(result);
        });
    }

    calculateAverageY(results: OptimizationResult[]): number {
        const totalY = results.reduce((sum, result) => sum + result.y, 0);
        return totalY / results.length;
    }

    calculateRankedResults(): void {
        const averageResults: {
            [key: string]: { averageY: number; count: number; params: string };
        } = {};

        console.log("rankedResults");

        // average y i br rjesenja za svaku komb param
        Object.entries(this.groupedResults).forEach(([_, results]) => {
            const averageY = this.calculateAverageY(results);
            const count = results.length;
            const params = results[0].params;
            averageResults[params] = { averageY, count, params };
        });
        console.log(averageResults);

        // sortiram prema udalj od exact solution
        const sortedResults = Object.entries(averageResults).sort(
            ([, data1], [, data2]) => {
                console.log("data1", data1);
                console.log("data2", data2);
                const diff1 = Math.abs(data1.averageY - this.exactSolution);
                const diff2 = Math.abs(data2.averageY - this.exactSolution);
                return diff1 - diff2; // Sortiranje po rastućem redosledu razlika
            },
        );

        console.log("sortirani", sortedResults);

        // Konvertovanje nazad u željeni format
        const rankedResults: {
            [key: string]: { averageY: number; count: number; params: string };
        } = {};
        sortedResults.forEach(([params, { averageY, count }]) => {
            rankedResults[averageY] = { averageY, count, params };
        });

        // Postavljanje rankedResults na rezultate
        this.rankedResults = rankedResults;
    }
}
