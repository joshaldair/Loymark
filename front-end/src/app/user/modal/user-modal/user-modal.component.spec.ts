import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal, NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { UserModalComponent } from './user-modal.component';
import { UserService } from '../../service/user.service';
import { of } from 'rxjs';

describe('UserModalComponent', () => {
  let component: UserModalComponent;
  let fixture: ComponentFixture<UserModalComponent>;
  let service: UserService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        FormsModule, ReactiveFormsModule, HttpClientTestingModule, NgbModule,
      ],
      declarations: [UserModalComponent],
      providers: [
        UserService,
        NgbActiveModal,
        NgbModal
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    service = fixture.debugElement.injector.get(UserService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('formatDate', () => {
    const fecha = component.formatDate(new Date('01/01/1999'));
    expect(fecha).toEqual('1999-01-01')
  });

  it('onSubmit ', () => {
    component.form.controls['userName'].setValue('NA');
    component.form.controls['lastName'].setValue('NA');
    component.form.controls['country'].setValue('NAD');
    component.form.controls['contactInfo'].setValue(true);
    component.form.controls['email'].setValue('NA@gmail.com');
    component.form.controls['birthDay'].setValue(new Date());
    component.form.controls['id'].setValue(2);
    component.form.controls['cellphone'].setValue(2);

    const spy1 = spyOn(service, 'saveOrUpdateUsuario').and.returnValue(of());
    component.onSubmit();

    expect(spy1).toHaveBeenCalledWith(component.form.value);
  });
});
