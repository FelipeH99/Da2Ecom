import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyColorDiscountComponent } from './modify-color-discount.component';

describe('ModifyColorDiscountComponent', () => {
  let component: ModifyColorDiscountComponent;
  let fixture: ComponentFixture<ModifyColorDiscountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModifyColorDiscountComponent]
    });
    fixture = TestBed.createComponent(ModifyColorDiscountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
