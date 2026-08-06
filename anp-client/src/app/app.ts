import {TuiButton, TuiRoot} from "@taiga-ui/core";
import {TuiHeaderComponent, TuiMainComponent, TuiNavigation} from "@taiga-ui/layout";
import {ChangeDetectionStrategy, Component} from "@angular/core";
import { RouterLink, RouterOutlet } from "@angular/router";
import { ArticleComponent } from "./articles/article.component";
import routeConfig from "../route-config";

import { ArticlesDetailsComponent } from "../views/articles-details-component/articles-details-component";

@Component({
  selector: "app-root",
  imports: [RouterOutlet, RouterLink, TuiRoot, TuiMainComponent, TuiHeaderComponent, TuiButton],
  templateUrl: "./app.html",
  styleUrl: "./app.css",
})
export class App {
  title: string = "Another News Platform";
}
