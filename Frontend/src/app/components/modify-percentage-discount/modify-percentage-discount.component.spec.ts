import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyPercentageDiscountComponent } from './modify-percentage-discount.component';

describe('ModifyPercentageDiscountComponent', () => {
  let component: ModifyPercentageDiscountComponent;
  let fixture: ComponentFixture<ModifyPercentageDiscountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModifyPercentageDiscountComponent]
    });
    fixture = TestBed.createComponent(ModifyPercentageDiscountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
