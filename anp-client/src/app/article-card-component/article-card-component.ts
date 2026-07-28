import {ChangeDetectionStrategy, Component, Input} from '@angular/core';
import { Router } from '@angular/router';
import {Article} from '../../models/article';
import { TuiAppearance, TuiButton, TuiTitle } from '@taiga-ui/core';
import {TuiCardLarge, TuiHeader} from '@taiga-ui/layout'
import {ArticleService} from '../../services/article-service';



@Component({
  selector: 'app-article-card-component',
  imports: [
    TuiCardLarge,
    TuiHeader,
    TuiTitle,
    TuiButton,
  ],
  templateUrl: './article-card-component.html',
  styleUrl: './article-card-component.css',
})
export class ArticleCardComponent {
  @Input() article!: Article;
}
