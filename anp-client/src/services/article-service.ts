import {Injectable, Service} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ArticlesMock } from './article-mocked';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  constructor(private http: HttpClient) { }

  getMockedArticles(){
    return ArticlesMock;
  }
}
