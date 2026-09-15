import { Component, inject } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Article } from '../../../models/article-model';
import { ArticleService } from '../../../services/article-service';
import { TuiButton } from '@taiga-ui/core';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout';

@Component({
  imports: [TuiButton, RouterLink, TuiCardLarge, TuiHeader],
  selector: 'app-article-details-page-component',
  styleUrl: './article-details-page-component.less',
  templateUrl: './article-details-page-component.html',
})
export class ArticleDetailsPageComponent {
  private readonly route = inject(ActivatedRoute);
  private readonly articleService = inject(ArticleService);

  article: Article | undefined = this.articleService.getById(
    this.route.snapshot.paramMap.get('id') ?? '',
  );
}
