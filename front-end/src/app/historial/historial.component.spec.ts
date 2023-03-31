import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HistorialComponent } from './historial.component';
import { HistorialService } from './service/historial.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { of } from 'rxjs';

describe('HistorialComponent', () => {
  let component: HistorialComponent;
  let fixture: ComponentFixture<HistorialComponent>;
  let service: HistorialService;
  const response = [
    {
      "userId": 0,
      "activityName": "string",
      "id": 0,
    }
  ];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgbModule
      ],
      declarations: [HistorialComponent],
      providers: [
        HistorialService
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HistorialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    service = fixture.debugElement.injector.get(HistorialService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('getActivity', () => {

    const spy = spyOn(service, 'getActivities').and.returnValue(of(response));
    component.getActivity();

    expect(component.activityList).toEqual(response);
    expect(spy).toHaveBeenCalled();
  });

  xit('onDelete', (done) => {

   
    component.deleteActivity(4);
    spyOn(window, 'confirm').and.returnValue(true);
    const spy = spyOn(service, 'deleteActividad').and.returnValue(Promise.resolve({}));

    setTimeout(() => {
      expect(spy).toHaveBeenCalled();
      done();
    });
  });
});
