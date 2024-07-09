import { Component, OnInit } from "@angular/core";
import { OptimizationResult } from "../../model/optimization-result.model";
import { OptimizationAnalyticsService } from "../optimization-analytics.service";
import { MatDialog } from "@angular/material/dialog";
import { OptimizerHistoryComponent } from "../optimizer-history/optimizer-history.component";
import { ChartComponent } from "../chart/chart.component";
import { faChartLine, faRankingStar } from "@fortawesome/free-solid-svg-icons";
import { ParamsGroupComponent } from "../params-group/params-group.component";
import { RankingComponent } from "../ranking/ranking.component";

@Component({
    selector: "app-analytics-home",
    templateUrl: "./analytics-home.component.html",
    styleUrls: ["./analytics-home.component.css"],
})
export class AnalyticsHomeComponent implements OnInit {
    results: OptimizationResult[] = [];
    groupedResults: { [key: number]: OptimizationResult[] } = {}; //grupisani po params hash codovima
    selectedProblemName: string = "";
    isProblemNameSelected: boolean = false;
    faChartLine = faChartLine;
    faRankingStar = faRankingStar;

    constructor(
        private service: OptimizationAnalyticsService,
        private dialog: MatDialog,
    ) {}

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
        this.isProblemNameSelected = true;
        console.log("Selected problem name:", this.selectedProblemName);
    }

    getUniqueOptimizersForProblem(): string[] {
        var uniqueNames = new Set<string>(); //set-da uzmem u obzir samo jedinstvene optimizerNames
        this.results.forEach(result => {
            if (
                this.parseJsonString(result.problemInfo).ProblemName ==
                this.selectedProblemName
            )
                uniqueNames.add(result.optimizerName);
        });
        return Array.from(uniqueNames);
    }

    openDialog(problemName: string, optimizerName: string): void {
        this.dialog.open(OptimizerHistoryComponent, {
            width: "70vw",
            height: "95vh",
            data: { problemName: problemName, optimizerName: optimizerName },
        });
    }

    showChart(problemName: string, optimizerName: string): void {
        this.dialog.open(ChartComponent, {
            width: "70vw",
            height: "95vh",
            data: { problemName: problemName, optimizerName: optimizerName },
        });
    }

    showParamsCombination(problemName: string, optimizerName: string): void {
        this.dialog.open(ParamsGroupComponent, {
            width: "70vw",
            height: "95vh",
            data: { problemName: problemName, optimizerName: optimizerName },
        });
    }

    showRanking(problemName: string, optimizerName: string): void {
        this.dialog.open(RankingComponent, {
            width: "70vw",
            height: "95vh",
            data: { problemName: problemName, optimizerName: optimizerName },
        });
    }
}
