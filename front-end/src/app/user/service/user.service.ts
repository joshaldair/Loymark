import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUsuarios = () => this.http.get(environment.apiUrl + 'user');

  deleteUsuario = (id: number) =>
    this.http.delete(environment.apiUrl + 'user/' + id).toPromise();

  saveOrUpdateUsuario = (formData: any) => {
    if (formData.id == 0)
      return this.http.post(environment.apiUrl + 'user', formData);
    else
      return this.http.put(environment.apiUrl + 'user', formData);
  }
}
