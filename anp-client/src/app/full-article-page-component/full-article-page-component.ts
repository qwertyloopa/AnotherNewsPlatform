import {Component, Input} from '@angular/core';
import { Article } from '../../models/article';

@Component({
  selector: 'app-full-article-page-component',
  imports: [],
  templateUrl: './full-article-page-component.html',
  styleUrl: './full-article-page-component.css',
})
export class FullArticlePageComponent {
 @Input() article!: Article;

}
