import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { mockedArticles } from '../models/mocked-article';

@Injectable({
  providedIn: 'root',
})

export class ArticleService {
  constructor(private http: HttpClient) {}

  getMockedArticles() {
    let articles = mockedArticles;
    return articles;
  }

}
