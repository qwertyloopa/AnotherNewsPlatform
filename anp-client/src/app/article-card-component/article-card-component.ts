import { ChangeDetectionStrategy, Component } from '@angular/core';
import { TuiPlatform } from '@taiga-ui/cdk';
import { Router } from '@angular/router';
import {Article} from '../../models/article';
import { TuiAppearance, TuiButton, TuiTitle } from '@taiga-ui/core';

@Component({
  selector: 'app-article-card-component',
  imports: [
    TuiPlatform,
    TuiAppearance,
    TuiTitle,
    TuiButton
  ],
  templateUrl: './article-card-component.html',
  styleUrl: './article-card-component.css',
})
export class ArticleCardComponent {
  article!: Article;
}
