import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateColorDiscountComponent } from './create-color-discount.component';

describe('CreateColorDiscountComponent', () => {
  let component: CreateColorDiscountComponent;
  let fixture: ComponentFixture<CreateColorDiscountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateColorDiscountComponent]
    });
    fixture = TestBed.createComponent(CreateColorDiscountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
