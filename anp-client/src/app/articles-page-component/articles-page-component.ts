import {Component, inject} from '@angular/core';
import {ArticleService} from '../../services/article-service';
import {Article} from '../../models/article';
import {ArticleCardComponent} from '../article-card-component/article-card-component';

@Component({
  selector: 'app-articles-page-component',
  imports: [
    ArticleCardComponent
  ],
  templateUrl: './articles-page-component.html',
  styleUrl: './articles-page-component.css',
})
export class ArticlesPageComponent {
  articles: Article[] = [];
  constructor(private articleService: ArticleService) {
    this.articleService = inject(ArticleService);
  }
  ngOnInit() {
    this.getArticles()
  }

  getArticles() {
    this.articles = this.articleService.getMockedArticles()
  }

}
