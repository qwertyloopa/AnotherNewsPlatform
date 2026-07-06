import { Injectable } from '@angular/core';
import { Article } from '../../models/article';
import { ARTICLES } from '../../models/articles';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class ArticlesService {
  constructor(private http: HttpClient) {}
  getArticles(): Article[] {
    return ARTICLES;
  }

  getArticleById(id: string): Article | undefined {
    return ARTICLES.find((item) => item.id === id);
  }

  getArticlesFromApi(): Observable<Article[] | null> {
    let url: string = 'https://localhost:7238/api/News/GetByRateAndSource';
    return this.http.get<Article[] | null>(url);
  }
}
