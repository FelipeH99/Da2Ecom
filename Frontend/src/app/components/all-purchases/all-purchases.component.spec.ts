import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllPurchasesComponent } from './all-purchases.component';

describe('AllPurchasesComponent', () => {
  let component: AllPurchasesComponent;
  let fixture: ComponentFixture<AllPurchasesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AllPurchasesComponent]
    });
    fixture = TestBed.createComponent(AllPurchasesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
