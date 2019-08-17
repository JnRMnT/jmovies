import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieNavigationComponent } from './movie-navigation.component';

describe('MovieNavigationComponent', () => {
  let component: MovieNavigationComponent;
  let fixture: ComponentFixture<MovieNavigationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MovieNavigationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MovieNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
