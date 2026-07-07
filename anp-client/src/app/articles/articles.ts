import { Component } from '@angular/core';
import { Article } from '../../models/article';
import { ArticleService } from '../../services/article-service';
import {ArticleCardComponent} from '../article-card-component/article-card-component';

@Component({
  selector: 'app-articles',
  imports: [],
  templateUrl: './articles.html',
  styleUrl: './articles.css',
})
export class Articles {
  Articles: Article[] = [];
  ArticleCards: ArticleCardComponent[] = [];
  constructor(private articleService: ArticleService) {
    this.getArticles();
  }

  getArticles(): void {
    this.Articles = this.articleService.getMockedArticles();
    this.ArticleCards
  }
}
