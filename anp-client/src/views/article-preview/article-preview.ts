import { Component, Input } from '@angular/core';
import { ArticleComponent } from '../../app/articles/article.component';
import { Article } from '../../models/article';
import {CommonModule, NgComponentOutlet} from '@angular/common';
import { RouterLink } from '@angular/router';
import {TuiAppearance} from "@taiga-ui/core";


@Component({
  selector: 'app-article-preview',
  imports: [CommonModule, RouterLink, TuiAppearance],
  templateUrl: './article-preview.html',
  styleUrl: './article-preview.css',
})
export class ArticlePreviewComponent {
  @Input() article?: Article;
}
