import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AnalyticsHomeComponent } from "src/app/feature-modules/optimization-analytics/analytics-home/analytics-home.component";

const routes: Routes = [{ path: "", component: AnalyticsHomeComponent }];

@NgModule({
    imports: [
        RouterModule.forRoot(routes, { scrollPositionRestoration: "enabled" }),
    ],
    exports: [RouterModule],
})
export class AppRoutingModule {}
