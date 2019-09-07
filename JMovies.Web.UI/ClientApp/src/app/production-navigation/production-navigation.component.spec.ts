import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductionNavigationComponent } from './production-navigation.component';

describe('ProductionNavigationComponent', () => {
  let component: ProductionNavigationComponent;
  let fixture: ComponentFixture<ProductionNavigationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ProductionNavigationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductionNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
