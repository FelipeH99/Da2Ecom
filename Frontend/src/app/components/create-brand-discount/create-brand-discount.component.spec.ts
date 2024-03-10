import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBrandDiscountComponent } from './create-brand-discount.component';

describe('CreateBrandDiscountComponent', () => {
  let component: CreateBrandDiscountComponent;
  let fixture: ComponentFixture<CreateBrandDiscountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateBrandDiscountComponent]
    });
    fixture = TestBed.createComponent(CreateBrandDiscountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
