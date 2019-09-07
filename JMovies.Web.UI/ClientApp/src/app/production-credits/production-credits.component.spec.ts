import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductionCreditsComponent } from './production-credits.component';

describe('ProductionCreditsComponent', () => {
  let component: ProductionCreditsComponent;
  let fixture: ComponentFixture<ProductionCreditsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ProductionCreditsComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductionCreditsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
