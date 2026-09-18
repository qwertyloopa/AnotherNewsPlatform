import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { ArticlePageComponent } from './components/pages/article-page-component/article-page-component';
import { ArticleDetailsPageComponent } from './components/pages/article-details-page-component/article-details-page-component';
import { ArticleService } from './services/article-service';
import { catchError, map } from 'rxjs';
import { LoginPageComponent } from './components/pages/login-page-component/login-page-component';

export const routes: Routes = [
  { path: '', component: ArticlePageComponent, title: 'Main | Another News Platform' },
  { path: 'article', redirectTo: '', pathMatch: 'full' },
  {
    path: 'article/:id',
    component: ArticleDetailsPageComponent,
    title: (route) => inject(ArticleService).getArticleById(route.paramMap.get('id') ?? '')
      .pipe(map((article) => `${article.title} | Another News Platform`), catchError(() => 'Article | Another News Platform'))
  },
  { path: 'login', component: LoginPageComponent, title: 'Login | Another News Platform' , pathMatch: 'full'}
];
