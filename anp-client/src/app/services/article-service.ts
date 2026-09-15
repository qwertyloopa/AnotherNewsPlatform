import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { mockedArticles } from '../models/mocked-article';
import { Article } from '../models/article-model';

@Injectable({
  providedIn: 'root',
})

export class ArticleService {
  constructor(private http: HttpClient) {}

  getMockedArticles() {
    let articles = mockedArticles;
    return articles;
  }

  getById(id: string): Article | undefined {
    return mockedArticles.find((article) => article.id === id);
  }

}
