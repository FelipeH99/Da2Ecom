import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePercentageDiscountComponent } from './create-percentage-discount.component';

describe('CreatePercentageDiscountComponent', () => {
  let component: CreatePercentageDiscountComponent;
  let fixture: ComponentFixture<CreatePercentageDiscountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreatePercentageDiscountComponent]
    });
    fixture = TestBed.createComponent(CreatePercentageDiscountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
