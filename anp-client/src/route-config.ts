import { Routes } from '@angular/router';
import { ArticleComponent } from './app/articles/article.component';
import { ArticlesDetailsComponent } from './views/articles-details-component/articles-details-component';
import { LoginFormComponent } from './views/login-form-component/login-form-component';
import { RegisterFormComponent } from './views/register-form-component/register-form-component';

const routeConfig: Routes = [
  { path: '', component: ArticleComponent, title: 'Main Page | Another News Platform' },
  { path: 'articles', redirectTo: '', pathMatch: 'full' },
  {
    path: 'articles/:id',
    component: ArticlesDetailsComponent,
    title: `article.title | Another News Platform`, // вкинуть название новости
  },
  { path: 'login', component: LoginFormComponent, title: 'Login | Another News Platform' },
  { path: 'register', component: RegisterFormComponent, title: 'Register | Another News Platform', },
];
export default routeConfig;
