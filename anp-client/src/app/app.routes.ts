import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { ArticlePageComponent } from './components/pages/article-page-component/article-page-component';
import { ArticleDetailsPageComponent } from './components/pages/article-details-page-component/article-details-page-component';
import { ArticleService } from './services/article-service';

export const routes: Routes = [
  { path: '', component: ArticlePageComponent, title: 'Main | Another News Platform' },
  { path: 'article', redirectTo: '', pathMatch: 'full' },
  {
    path: 'article/:id',
    component: ArticleDetailsPageComponent,
    title: (route) => {
      const id = route.paramMap.get('id');
      const article = inject(ArticleService).getById(id ?? '');
      return article ? `${article.title} | Another News Platform` : 'Article | Another News Platform';
    },
  },
];
