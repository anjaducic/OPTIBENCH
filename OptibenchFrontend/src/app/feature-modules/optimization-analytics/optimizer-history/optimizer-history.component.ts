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
}
