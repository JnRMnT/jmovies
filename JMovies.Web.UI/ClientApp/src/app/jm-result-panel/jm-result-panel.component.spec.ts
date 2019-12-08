import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JmResultPanelComponent } from './jm-result-panel.component';

describe('JmResultPanelComponent', () => {
  let component: JmResultPanelComponent;
  let fixture: ComponentFixture<JmResultPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JmResultPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JmResultPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
