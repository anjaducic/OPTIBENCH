import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";

@Component({
    selector: "app-optimizer-history",
    templateUrl: "./optimizer-history.component.html",
    styleUrls: ["./optimizer-history.component.css"],
})
export class OptimizerHistoryComponent {
    constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}
}
