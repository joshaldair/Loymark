import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HistorialService } from './historial.service';
import { environment } from 'src/environments/environment';
import { Actividad } from './actividad.model';


describe('HistorialService', () => {
  let service: HistorialService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
    });
    service = TestBed.inject(HistorialService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('Save', () => {
    const obj: Actividad = {
      userId: 1,
      activityName: 'string',
      id: 0
    }

    service.saveOrUpdateActividad(obj).subscribe();
    const req = httpMock.expectOne(environment.apiUrl + 'activity');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(obj);
    req.flush({});
  });

  it('UPdate', () => {
    const obj: Actividad = {
      userId: 14,
      activityName: 'string',
      id: 54
    }

    service.saveOrUpdateActividad(obj).subscribe();
    const req = httpMock.expectOne(environment.apiUrl + 'activity');
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(obj);
    req.flush({});
  });

  it('Delete', () => {
    service.deleteActividad(2).then();
    const req = httpMock.expectOne(environment.apiUrl + 'activity/' + 2);
    expect(req.request.method).toBe('DELETE');
    expect(req.request.urlWithParams).toContain('2')
    req.flush({});
  });

  it('Get User List', () => {
    service.getUsuarios().subscribe();
    const req = httpMock.expectOne(environment.apiUrl + 'user');
    expect(req.request.method).toBe('GET');
    req.flush({});
  });

  it('Get Activity List', () => {
    service.getActivities().subscribe();
    const req = httpMock.expectOne(environment.apiUrl + 'activity');
    expect(req.request.method).toBe('GET');
    req.flush({});
  });
});
