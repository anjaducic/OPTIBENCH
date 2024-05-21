import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";

import { AppModule } from "./app/app.module";
import "chartjs-plugin-annotation"; //za iscrtavanje tacnog rjesenja na histogramu

platformBrowserDynamic()
    .bootstrapModule(AppModule)
    .catch(err => console.error(err));
