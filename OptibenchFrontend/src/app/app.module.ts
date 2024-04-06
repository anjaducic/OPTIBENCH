import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./infrastructure/routing/app-routing.module";
import { AppComponent } from "./app.component";
import { AnalyticsHomeComponent } from "./feature-modules/optimization-analytics/analytics-home/analytics-home.component";
import { OptimizationAnalyticsModule } from "./feature-modules/optimization-analytics/optimization-analytics.module";
import { HttpClientModule } from "@angular/common/http";
import { MaterialModule } from "./infrastructure/material/material.module";
import { FormsModule } from "@angular/forms";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatCardModule } from "@angular/material/card";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        OptimizationAnalyticsModule,
        MaterialModule,
        FormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatCardModule,
        FontAwesomeModule,
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
