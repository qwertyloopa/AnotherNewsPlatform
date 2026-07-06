import { TuiRoot } from '@taiga-ui/core';
import { Component, signal } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { ArticleComponent } from './articles/article.component';
import routeConfig from '../route-config';

import { ArticlesDetailsComponent } from '../views/articles-details-component/articles-details-component';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, TuiRoot],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  title: string = 'Another News Platform';
}
