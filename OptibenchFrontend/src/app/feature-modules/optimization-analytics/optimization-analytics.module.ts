import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AnalyticsHomeComponent } from "./analytics-home/analytics-home.component";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatOptionModule } from "@angular/material/core";
import { MaterialModule } from "src/app/infrastructure/material/material.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatSelectModule } from "@angular/material/select";
import { OptimizerHistoryComponent } from "./optimizer-history/optimizer-history.component";
import { ChartComponent } from "./chart/chart.component";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { ParamsGroupComponent } from './params-group/params-group.component';
import { ChartByParamsComponent } from './chart-by-params/chart-by-params.component';

@NgModule({
    declarations: [
        AnalyticsHomeComponent,
        OptimizerHistoryComponent,
        ChartComponent,
        ParamsGroupComponent,
        ChartByParamsComponent,
    ],
    imports: [
        CommonModule,
        MatFormFieldModule,
        MatOptionModule,
        MaterialModule,
        ReactiveFormsModule,
        FormsModule,
        MatSelectModule,
        FontAwesomeModule,
    ],
})
export class OptimizationAnalyticsModule {}
