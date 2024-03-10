import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyYourUserComponent } from './modify-your-user.component';

describe('ModifyYourUserComponent', () => {
  let component: ModifyYourUserComponent;
  let fixture: ComponentFixture<ModifyYourUserComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModifyYourUserComponent]
    });
    fixture = TestBed.createComponent(ModifyYourUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
