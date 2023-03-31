import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal, NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { of } from 'rxjs';
import { HistorialService } from '../../service/historial.service';

import { HistorialModalComponent } from './historial-modal.component';

describe('HistorialModalComponent', () => {
  let component: HistorialModalComponent;
  let fixture: ComponentFixture<HistorialModalComponent>;
  let service: HistorialService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        FormsModule, ReactiveFormsModule, HttpClientTestingModule, NgbModule,
      ],
      declarations: [ HistorialModalComponent ],
      providers: [
        HistorialService,
        NgbActiveModal,
        NgbModal
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HistorialModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    service = fixture.debugElement.injector.get(HistorialService);
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

    expect(component.listaUsuarios).toEqual(response);
    expect(spy).toHaveBeenCalled();
  });

  it('onSubmit ', () => {
    component.form.controls['activityName'].setValue('NA');
    component.form.controls['userId'].setValue(2);
    component.form.controls['id'].setValue(2);

    const spy1 = spyOn(service, 'saveOrUpdateActividad').and.returnValue(of());
    component.onSubmit();

    expect(spy1).toHaveBeenCalledWith(component.form.value);
  });
});
