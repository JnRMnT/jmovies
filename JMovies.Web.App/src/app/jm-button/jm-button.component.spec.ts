import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JmButtonComponent } from './jm-button.component';

describe('JmButtonComponent', () => {
  let component: JmButtonComponent;
  let fixture: ComponentFixture<JmButtonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JmButtonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JmButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
