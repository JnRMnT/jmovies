import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductionPreviewComponent } from './production-preview.component';

describe('ProductionPreviewComponent', () => {
  let component: ProductionPreviewComponent;
  let fixture: ComponentFixture<ProductionPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductionPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductionPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
