import { Component, inject } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Article } from '../../../models/article-model';
import { ArticleService } from '../../../services/article-service';
import { TuiButton, TuiIcon, TuiInput } from '@taiga-ui/core';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';
import { TuiTextarea } from '@taiga-ui/kit';
import { toSignal } from '@angular/core/rxjs-interop';
import { catchError, map, of, switchMap } from 'rxjs';

@Component({
  imports: [TuiButton, TuiCardLarge, TuiHeader, TuiInput, TuiTextarea],
  selector: 'app-article-details-page-component',
  styleUrl: './article-details-page-component.less',
  templateUrl: './article-details-page-component.html',
})
export class ArticleDetailsPageComponent {
  private readonly route = inject(ActivatedRoute);
  private readonly articleService = inject(ArticleService);

  protected readonly article = toSignal(
    this.route.paramMap.pipe(
      map((params) => params.get('id') ?? ''),
      switchMap((id) => this.articleService.getArticleById(id)),
      catchError(() => of(undefined)),
    ),
  );
}
