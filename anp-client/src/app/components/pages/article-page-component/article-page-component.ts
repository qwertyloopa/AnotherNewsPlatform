import { Component, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
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

  articleService = inject(ArticleService);

  articles = toSignal(this.articleService.getArticles(), { initialValue: [] as Article[] });
}




