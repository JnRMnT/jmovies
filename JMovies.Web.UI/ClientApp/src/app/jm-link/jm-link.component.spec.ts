import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JmLinkComponent } from './jm-link.component';

describe('JmLinkComponent', () => {
  let component: JmLinkComponent;
  let fixture: ComponentFixture<JmLinkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JmLinkComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JmLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
