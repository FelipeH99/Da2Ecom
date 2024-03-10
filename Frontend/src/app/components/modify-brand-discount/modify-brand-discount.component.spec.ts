import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyBrandDiscountComponent } from './modify-brand-discount.component';

describe('ModifyBrandDiscountComponent', () => {
  let component: ModifyBrandDiscountComponent;
  let fixture: ComponentFixture<ModifyBrandDiscountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModifyBrandDiscountComponent]
    });
    fixture = TestBed.createComponent(ModifyBrandDiscountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
