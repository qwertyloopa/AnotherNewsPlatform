import { Routes } from '@angular/router';
import {Article} from '../models/article';
import { ArticlesPageComponent } from './articles-page-component/articles-page-component';
import {FullArticlePageComponent} from './full-article-page-component/full-article-page-component';

export const routes: Routes = [
  {
    path: '', title: "News | ANP", component: ArticlesPageComponent
  },
  {
    path: 'articles', redirectTo: '/',
  },
  {
    path : 'articles/:articleId', title: `ArticleNameHere | ANP`, component: FullArticlePageComponent
  }
];
