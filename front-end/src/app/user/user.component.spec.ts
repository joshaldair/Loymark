import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserService } from './service/user.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { UserComponent } from './user.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { of } from 'rxjs';

describe('UserComponent', () => {
  let component: UserComponent;
  let fixture: ComponentFixture<UserComponent>;
  let service: UserService;


  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgbModule
      ],
      declarations: [UserComponent],
      providers: [
        UserService
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    service = fixture.debugElement.injector.get(UserService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('getUsuarios ', () => {
    const response = {
      "userName": "Cecillion",
      "lastName": "Charmin",
      "email": null,
      "birthDay": "0001-01-01T00:00:00",
      "cellphone": null,
      "country": null,
      "contactInfo": false,
      "id": 0
    }
    const spy = spyOn(service, 'getUsuarios').and.returnValue(of(response));
    component.getUsuarios();

    expect(component.listadoUsuarios).toEqual(response);
    expect(spy).toHaveBeenCalled();
  });


});

