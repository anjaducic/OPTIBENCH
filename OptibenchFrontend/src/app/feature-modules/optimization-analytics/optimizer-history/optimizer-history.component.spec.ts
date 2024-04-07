import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OptimizerHistoryComponent } from './optimizer-history.component';

describe('OptimizerHistoryComponent', () => {
  let component: OptimizerHistoryComponent;
  let fixture: ComponentFixture<OptimizerHistoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OptimizerHistoryComponent]
    });
    fixture = TestBed.createComponent(OptimizerHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
