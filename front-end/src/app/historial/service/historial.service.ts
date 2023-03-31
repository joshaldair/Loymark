import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Actividad } from './actividad.model';

@Injectable({
  providedIn: 'root'
})
export class HistorialService {

  constructor(private http: HttpClient) { }

  getActivities = () =>
    this.http.get(environment.apiUrl + 'activity');

  getUsuarios = () => this.http.get(environment.apiUrl + 'user');

  saveOrUpdateActividad = (formData: Actividad) => {
    if (formData.id == 0)
      return this.http.post(environment.apiUrl + 'activity', formData);
    else
      return this.http.put(environment.apiUrl + 'activity', formData);
  }

  deleteActividad = (id: number) =>
    this.http.delete(environment.apiUrl + 'activity/' + id).toPromise();


}
