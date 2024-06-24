import {
    Component,
    ElementRef,
    Inject,
    OnInit,
    ViewChild,
} from "@angular/core";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { OptimizationAnalyticsService } from "../optimization-analytics.service";
import { OptimizationResult } from "../../model/optimization-result.model";
import Chart, {
    ChartConfiguration,
    ChartTypeRegistry,
    Plugin,
} from "chart.js/auto";
import { Range } from "../../model/range.model";
import { AnyObject } from "chart.js/types/basic";

@Component({
    selector: "app-chart-by-params",
    templateUrl: "./chart-by-params.component.html",
    styleUrls: ["./chart-by-params.component.css"],
})
export class ChartByParamsComponent implements OnInit {
    results: OptimizationResult[] = [];
    exactSolution: number = 0;
    chart: any = [];
    xRanges: Range[] = [];
    labels: string[] = [];
    dataSets: number[] = [];
    yMin: number = 0;
    yMax: number = 0;

    canvas: any;

    constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}

    ngOnInit(): void {
        this.results = this.data.results;
        this.exactSolution = this.results[0].exactSolution;
        console.log(this.results);
        if (this.results.length > 0) {
            this.findYBounds();
            this.findXRanges();
            this.calculateDataSet();
            this.createChart();
        }
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
            const rangeSize = (this.yMax - this.yMin) / 5; //provjeriti za negativne
            for (let i = 0; i < 5; i++) {
                if (i === 4)
                    var range: Range = {
                        start: this.yMin + i * rangeSize,
                        end: this.yMax,
                    };
                else
                    var range: Range = {
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
                      return `to ${range.end}`;
                  });
    }

    private calculateDataSet(): void {
        if (this.results.length == 1) {
            this.dataSets = Array(1).fill(1);
        } else {
            this.dataSets = Array(5).fill(0);
            for (let result of this.results) {
                for (let i = 0; i < 5; i++) {
                    if (
                        result.y >= this.xRanges[i].start &&
                        (result.y < this.xRanges[i].end ||
                            (i === 4 && result.y <= this.xRanges[i].end))
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

        const config: ChartConfiguration<"scatter" | "bar", number[], string> =
            {
                type: chartType,
                data: {
                    labels: this.labels,
                    datasets: [
                        {
                            label: "# number of results",
                            data: this.dataSets,
                            borderWidth: 1,
                            backgroundColor: this.dataSets.map(y =>
                                this.generateColor(y),
                            ),
                            pointRadius: 10, // veca tacka
                            pointHoverRadius: 12,
                        },
                    ],
                },
                options: {
                    scales: {
                        x: {
                            min: this.xRanges[0].start,
                        },
                        y: {
                            beginAtZero: true,
                            ticks: {
                                stepSize: 1,
                            },
                        },
                    },
                },
            };

        const ctx = document.getElementById("myChart") as HTMLCanvasElement;
        new Chart(ctx, config);
    }

    private generateColor(y: number): string {
        if (this.results.length === 1) {
            return "rgba(124, 77, 255, 0.8)";
        }
        const sortedDataSets = this.dataSets.slice().sort((a, b) => b - a);
        const index = sortedDataSets.findIndex(value => value === y);
        const percentPosition = index / (this.dataSets.length - 1);

        // raxunam alfa kanal - lin interpolacija izmedju 0.9 i 0.1
        const alpha = 0.8 - percentPosition * 0.7;

        // boja sa izr alfa kanalom
        const color = `rgba(124, 77, 255, ${alpha})`;

        return color;
    }
}
