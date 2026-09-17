import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { mockedArticles } from '../models/mocked-article';
import { Article } from '../models/article-model';
import { Observable } from 'rxjs';

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

  getArticles(): Observable<Article[]> {
    return this.http.get<Article[]>('https://localhost:7238/api/News/get-articles');
  }

  getArticleById(id: string): Observable<Article> {
    return this.http.get<Article>(`https://localhost:7238/api/News/${id}`);
  }

}
