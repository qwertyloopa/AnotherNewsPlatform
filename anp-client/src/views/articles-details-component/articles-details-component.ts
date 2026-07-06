import { Component, Injector, OnInit } from '@angular/core';
import { Article } from '../../models/article';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ArticlesService } from '../../services/articles/articles-service';
import {Location} from '@angular/common';

@Component({
  selector: 'app-articles-details-component',
  imports: [RouterLink],
  templateUrl: './articles-details-component.html',
  styleUrl: './articles-details-component.css',
})
export class ArticlesDetailsComponent implements OnInit {
  id?: string;
  article?: Article = null as any; //потом поменять


  constructor(
    private route: ActivatedRoute,
    private articleService: ArticlesService,
    private location: Location,
  ) {}
  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    console.log('id', this.id);

    this.article = this.articleService.getArticleById(this.id!);
  }


}
