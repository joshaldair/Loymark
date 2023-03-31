import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UserService } from './user.service';
import { environment } from 'src/environments/environment';
import { User } from './user.model';

describe('UserService', () => {
  let service: UserService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
    });
    service = TestBed.inject(UserService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('Get User List', () => {
    service.getUsuarios().subscribe();
    const req = httpMock.expectOne(environment.apiUrl + 'user');
    expect(req.request.method).toBe('GET');
    req.flush({});
  });

  it('Save', () => {
    const obj: User = {
      userName: 'string;',
      lastName: 'string;',
      email: 'string;',
      birthDay: new Date(),
      cellphone: 234,
      country: 'COL',
      contactInfo: true,
      id: 0
    }

    service.saveOrUpdateUsuario(obj).subscribe();
    const req = httpMock.expectOne(environment.apiUrl + 'user');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(obj);
    req.flush({});
  });

  it('Update', () => {
    const obj: User = {
      userName: 'string;',
      lastName: 'string;',
      email: 'string;',
      birthDay: new Date(),
      cellphone: 234,
      country: 'COL',
      contactInfo: true,
      id: 54
    }

    service.saveOrUpdateUsuario(obj).subscribe();
    const req = httpMock.expectOne(environment.apiUrl + 'user');
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(obj);
    req.flush({});
  });

  it('Delete', () => {
    service.deleteUsuario(2).then();
    const req = httpMock.expectOne(environment.apiUrl + 'user/' + 2);
    expect(req.request.method).toBe('DELETE');
    expect(req.request.urlWithParams).toContain('2')
    req.flush({});
  });
});
