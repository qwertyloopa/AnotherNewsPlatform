import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticlePreview } from './article-preview';

describe('ArticlePreview', () => {
  let component: ArticlePreview;
  let fixture: ComponentFixture<ArticlePreview>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ArticlePreview],
    }).compileComponents();

    fixture = TestBed.createComponent(ArticlePreview);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
