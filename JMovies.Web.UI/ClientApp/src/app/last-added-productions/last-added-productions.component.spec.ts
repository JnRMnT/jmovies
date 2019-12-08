import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LastAddedProductionsComponent } from './last-added-productions.component';

describe('LastAddedProductionsComponent', () => {
  let component: LastAddedProductionsComponent;
  let fixture: ComponentFixture<LastAddedProductionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LastAddedProductionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LastAddedProductionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
