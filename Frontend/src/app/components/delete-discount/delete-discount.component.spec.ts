import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteDiscountComponent } from './delete-discount.component';

describe('DeleteDiscountComponent', () => {
  let component: DeleteDiscountComponent;
  let fixture: ComponentFixture<DeleteDiscountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteDiscountComponent]
    });
    fixture = TestBed.createComponent(DeleteDiscountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
