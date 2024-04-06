import { Component, OnInit } from "@angular/core";
import { OptimizationResult } from "../../model/optimization-result.model";
import { OptimizationAnalyticsService } from "../optimization-analytics.service";

@Component({
    selector: "app-analytics-home",
    templateUrl: "./analytics-home.component.html",
    styleUrls: ["./analytics-home.component.css"],
})
export class AnalyticsHomeComponent implements OnInit {
    results: OptimizationResult[] = [];

    constructor(private service: OptimizationAnalyticsService) {}

    ngOnInit(): void {
        this.service.getResults().subscribe({
            next: (results: OptimizationResult[]) => {
                this.results = results;
            },
        });
    }
}
