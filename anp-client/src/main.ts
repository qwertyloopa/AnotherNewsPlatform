import { provideTaiga } from "@taiga-ui/core";
import { bootstrapApplication } from "@angular/platform-browser";
import { appConfig } from "./app/app.config";
import { App } from "./app/app";
import { provideRouter } from "@angular/router";
import routeConfig from "./route-config";

bootstrapApplication(App, {
  providers: [provideRouter(routeConfig), provideTaiga()],
}).catch((err) => console.error(err));
