import { Component } from '@angular/core';
import { ArticleService } from '../../../services/article-service';
import { Article } from '../../../models/article-model';
import { ArticleCardComponent } from '../../ui/article-card-component/article-card-component';

@Component({
  imports: [ArticleCardComponent],
  selector: 'app-article-page-component',
  styleUrl: './article-page-component.less',
  templateUrl: './article-page-component.html',
})
export class ArticlePageComponent {
  articles: Article[] = [];
  constructor(
    private articleService: ArticleService,
  ) {}

  ngOnInit() {
    this.articles = this.articleService.getMockedArticles();
  }
}




