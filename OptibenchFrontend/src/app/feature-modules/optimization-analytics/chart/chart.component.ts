import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { OptimizationAnalyticsService } from "../optimization-analytics.service";
import { OptimizationResult } from "../../model/optimization-result.model";
import { Chart } from "chart.js/auto";
import { Range } from "../../model/range.model";

@Component({
    selector: "app-chart",
    templateUrl: "./chart.component.html",
    styleUrls: ["./chart.component.css"],
})
export class ChartComponent implements OnInit {
    results: OptimizationResult[] = [];
    chart: any = [];
    xRanges: Range[] = [];
    labels: string[] = [];
    dataSets: number[] = [];
    yMin: number = 0;
    yMax: number = 0;

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
                    this.findYBounds();
                    this.findXRanges();
                    this.calculateDataSet();
                    this.createChart();
                },
            });
    }

    private findYBounds(): void {
        if (this.results != null && this.results.length > 0) {
            const sortedResults = this.results
                .slice()
                .sort((a, b) => a.y - b.y);

            this.yMin = sortedResults[0].y;
            this.yMax = sortedResults[sortedResults.length - 1].y;
        }
    }

    private findXRanges(): void {
        if (this.results.length === 1) {
            const singleRange: Range = {
                start: this.results[0].y,
                end: this.results[0].y,
            };
            this.xRanges.push(singleRange);
        } else {
            const rangeSize = (this.yMax - this.yMin) / 10; //provjeriti za negativne
            for (let i = 0; i < 10; i++) {
                const range: Range = {
                    start: this.yMin + i * rangeSize,
                    end: this.yMin + (i + 1) * rangeSize,
                };
                this.xRanges.push(range);
            }
        }
        this.labels =
            this.results.length === 1
                ? [this.results[0].y.toString()]
                : this.xRanges.map((range, index) => {
                      return `${range.start}`;
                  });
    }

    private calculateDataSet(): void {
        if (this.results.length == 1) {
            this.dataSets = Array(1).fill(1);
        } else {
            this.dataSets = Array(10).fill(0);
            for (let result of this.results) {
                for (let i = 0; i < 10; i++) {
                    if (
                        result.y >= this.xRanges[i].start &&
                        (result.y < this.xRanges[i].end ||
                            (i === 9 && result.y <= this.xRanges[i].end))
                    ) {
                        this.dataSets[i]++;
                        break;
                    }
                }
            }
        }
    }

    private createChart(): void {
        const chartType = this.results.length === 1 ? "scatter" : "bar";
        this.chart = new Chart("canvas", {
            type: chartType,
            data: {
                labels: this.labels,
                datasets: [
                    {
                        label: "# number of solutions",
                        data: this.dataSets,
                        borderWidth: 1,
                    },
                ],
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            stepSize: 1, // cjelobrojne vrijednoti na y
                        },
                    },
                },
            },
        });
    }
}
