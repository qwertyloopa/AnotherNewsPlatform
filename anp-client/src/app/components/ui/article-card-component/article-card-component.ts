import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TuiCard, TuiHeader } from '@taiga-ui/layout';
import { TuiButton } from '@taiga-ui/core';
import { Article } from '../../../models/article-model';
import { ArticleService } from '../../../services/article-service';
import { TuiRating } from '@taiga-ui/kit';
import { FormsModule } from '@angular/forms';

@Component({
  imports: [TuiCard, TuiHeader, TuiButton, RouterLink, TuiRating, FormsModule],
  selector: 'app-article-card-component',
  styleUrl: './article-card-component.less',
  templateUrl: './article-card-component.html',
})
export class ArticleCardComponent {
  @Input() article!: Article;

  constructor(private articleService: ArticleService) {}
  getArticle() {
    return this.articleService.getById(this.article.id);
  }
}
