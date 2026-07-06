import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticlesDetailsComponent } from './articles-details-component';

describe('ArticlesDetailsComponent', () => {
  let component: ArticlesDetailsComponent;
  let fixture: ComponentFixture<ArticlesDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ArticlesDetailsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ArticlesDetailsComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
