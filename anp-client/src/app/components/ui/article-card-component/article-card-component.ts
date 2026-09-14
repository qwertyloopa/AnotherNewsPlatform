import { Component, Input } from '@angular/core';
import { TuiCard, TuiHeader } from '@taiga-ui/layout';
import { TuiButton } from '@taiga-ui/core';
import { Article } from '../../../models/article-model';
import { ArticleService } from '../../../services/article-service';

@Component({
  imports: [TuiCard, TuiHeader, TuiButton],
  selector: 'app-article-card-component',
  styleUrl: './article-card-component.less',
  templateUrl: './article-card-component.html',
})
export class ArticleCardComponent {
  @Input() article!: Article;
}
