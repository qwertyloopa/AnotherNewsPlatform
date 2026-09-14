import { Routes } from '@angular/router';
import { ArticlePageComponent } from './components/pages/article-page-component/article-page-component';
import { ArticleDetailsPageComponent } from './components/pages/article-details-page-component/article-details-page-component';

export const routes: Routes = [
  { path: '', component: ArticlePageComponent, title: 'Main | Another News Platform' },
  { path: 'article', redirectTo: '', pathMatch: 'full' },
  { path: 'article/:id', component: ArticleDetailsPageComponent  }
];
