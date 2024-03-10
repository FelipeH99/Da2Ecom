import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteUsersComponent } from './delete-users.component';

describe('DeleteUsersComponent', () => {
  let component: DeleteUsersComponent;
  let fixture: ComponentFixture<DeleteUsersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteUsersComponent]
    });
    fixture = TestBed.createComponent(DeleteUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
