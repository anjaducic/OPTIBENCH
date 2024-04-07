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
    selectedProblemName: string = "";

    constructor(private service: OptimizationAnalyticsService) {}

    ngOnInit(): void {
        this.service.getResults().subscribe({
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

    getUniqueProblemNames(): string[] {
        var uniqueNames = new Set<string>(); //set-da uzmem u obzir samo jedinstvene problemNames
        this.results.forEach(result => {
            uniqueNames.add(
                this.parseJsonString(result.problemInfo).ProblemName,
            );
        });
        return Array.from(uniqueNames);
    }

    chooseProblem(): void {
        console.log("Selected problem name:", this.selectedProblemName);
    }
}
