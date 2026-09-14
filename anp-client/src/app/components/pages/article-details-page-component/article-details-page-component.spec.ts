import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ArticleDetailsPageComponent } from './article-details-page-component';

describe('ArticleDetailsPageComponent', () => {
  let component: ArticleDetailsPageComponent;
  let fixture: ComponentFixture<ArticleDetailsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ArticleDetailsPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ArticleDetailsPageComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
