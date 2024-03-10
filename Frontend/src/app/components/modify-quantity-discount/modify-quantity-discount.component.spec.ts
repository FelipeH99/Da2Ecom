import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyQuantityDiscountComponent } from './modify-quantity-discount.component';

describe('ModifyQuantityDiscountComponent', () => {
  let component: ModifyQuantityDiscountComponent;
  let fixture: ComponentFixture<ModifyQuantityDiscountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModifyQuantityDiscountComponent]
    });
    fixture = TestBed.createComponent(ModifyQuantityDiscountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
