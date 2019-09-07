import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductionCastComponent } from './production-cast.component';

describe('ProductionCastComponent', () => {
  let component: ProductionCastComponent;
  let fixture: ComponentFixture<ProductionCastComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ProductionCastComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductionCastComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
