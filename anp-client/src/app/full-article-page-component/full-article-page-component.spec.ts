import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FullArticlePageComponent } from './full-article-page-component';

describe('FullArticlePageComponent', () => {
  let component: FullArticlePageComponent;
  let fixture: ComponentFixture<FullArticlePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FullArticlePageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(FullArticlePageComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
