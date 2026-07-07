import { TuiRoot } from '@taiga-ui/core';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {ArticleCardComponent} from './article-card-component/article-card-component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TuiRoot, ArticleCardComponent],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('Another News Platform');
}
