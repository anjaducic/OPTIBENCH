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
import * as ChartAnnotation from "chartjs-plugin-annotation";
import { AnyObject } from "chart.js/types/basic";

type PluginAfterDrawCallback<T extends keyof ChartTypeRegistry> = (
    chart: Chart<T>,
) => void;

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
    exactSolution: number = 0;

    canvas: any;

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
                    if (this.results.length > 0) {
                        //this.exactSolution = this.results[0].exactSolution;
                        this.findYBounds();
                        this.findXRanges();
                        this.calculateDataSet();
                        this.createChart();
                    }
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
        //console.log(this.xRanges[9].end);
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
        const maxResult = Math.max(...this.dataSets);

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
                plugins: [
                    {
                        id: "myPlugin",
                        afterDraw: function (chart: Chart<"bar" | "scatter">) {
                            const ctx = chart.ctx;
                            const xAxis = chart.scales["y"];

                            const xValue = xAxis.getPixelForValue(-0.7);
                            ctx.save();
                            ctx.strokeStyle = "rgb(0, 255, 0)";
                            ctx.lineWidth = 2;
                            ctx.beginPath();
                            ctx.moveTo(xValue, chart.scales["y"].bottom); //pocni u dnu y
                            const top =
                                chart.scales["y"].getPixelForValue(maxResult); //visina max vrijednosti dataseta
                            ctx.lineTo(xValue, top);
                            ctx.stroke();
                            ctx.restore();
                        },
                    } as Plugin<"bar" | "scatter", AnyObject>,
                ],
            };

        const ctx = document.getElementById("myChart") as HTMLCanvasElement;
        new Chart(ctx, config);
    }
}
