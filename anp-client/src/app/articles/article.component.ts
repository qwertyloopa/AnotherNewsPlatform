import { ChangeDetectorRef, Component } from '@angular/core';
import { Article } from '../../models/article';
import { AsyncPipe, DatePipe } from '@angular/common';
import { mocked_articles } from '../../models/mocked_articles';
import { ArticlePreviewComponent } from '../../views/article-preview/article-preview';
import { ArticlesService } from '../../services/articles/articles-service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-articles',
  imports: [ArticlePreviewComponent, AsyncPipe],
  templateUrl: './article.component.html',
  styleUrl: './article.component.css',
})
export class ArticleComponent {
  articles: Observable<Article[] | null> = new Observable();

  constructor(
    private articlesService: ArticlesService,
    private ref: ChangeDetectorRef,
  ) {}
  ngOnInit() {
    this.articles = this.articlesService.getArticlesFromApi();
  }

  trackById(index: number, article: Article): string {
    return article.id;
  }
}
