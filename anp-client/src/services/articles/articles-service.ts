import { Injectable } from '@angular/core';
import { Article } from '../../models/article';
import { mocked_articles } from '../../models/mocked_articles';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class ArticlesService {
  constructor(private http: HttpClient) {}
  getArticles(): Article[] {
    return mocked_articles;
  }

  getArticleById(id: string): Article | undefined {
    return mocked_articles.find((item) => item.id === id);
  }

  getArticlesFromApi(): Observable<Article[] | null> {
    let url: string = 'https://localhost:7238/api/News/GetByRateAndSource';
    return this.http.get<Article[] | null>(url);
  }
}
