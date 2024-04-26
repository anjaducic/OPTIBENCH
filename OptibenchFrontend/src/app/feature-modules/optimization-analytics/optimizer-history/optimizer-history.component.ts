import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { OptimizationAnalyticsService } from "../optimization-analytics.service";
import { OptimizationResult } from "../../model/optimization-result.model";

@Component({
    selector: "app-optimizer-history",
    templateUrl: "./optimizer-history.component.html",
    styleUrls: ["./optimizer-history.component.css"],
})
export class OptimizerHistoryComponent implements OnInit {
    results: OptimizationResult[] = [];

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
                    this.results = results;
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
}
