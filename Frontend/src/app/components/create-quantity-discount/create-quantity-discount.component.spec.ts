import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateQuantityDiscountComponent } from './create-quantity-discount.component';

describe('CreateQuantityDiscountComponent', () => {
  let component: CreateQuantityDiscountComponent;
  let fixture: ComponentFixture<CreateQuantityDiscountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateQuantityDiscountComponent]
    });
    fixture = TestBed.createComponent(CreateQuantityDiscountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
