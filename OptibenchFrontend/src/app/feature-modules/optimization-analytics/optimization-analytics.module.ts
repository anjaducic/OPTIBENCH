import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AnalyticsHomeComponent } from "./analytics-home/analytics-home.component";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatOptionModule } from "@angular/material/core";
import { MaterialModule } from "src/app/infrastructure/material/material.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatSelectModule } from "@angular/material/select";
import { OptimizerHistoryComponent } from './optimizer-history/optimizer-history.component';
import { ChartComponent } from './chart/chart.component';

@NgModule({
    declarations: [AnalyticsHomeComponent, OptimizerHistoryComponent, ChartComponent],
    imports: [
        CommonModule,
        MatFormFieldModule,
        MatOptionModule,
        MaterialModule,
        ReactiveFormsModule,
        FormsModule,
        MatSelectModule,
    ],
})
export class OptimizationAnalyticsModule {}
