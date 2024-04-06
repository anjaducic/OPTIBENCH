import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./infrastructure/routing/app-routing.module";
import { AppComponent } from "./app.component";
import { AnalyticsHomeComponent } from "./feature-modules/optimization-analytics/analytics-home/analytics-home.component";
import { OptimizationAnalyticsModule } from "./feature-modules/optimization-analytics/optimization-analytics.module";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        OptimizationAnalyticsModule,
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
