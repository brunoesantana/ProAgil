import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

baseUrl = 'http://localhost:5000/api/eventos';

constructor(private http: HttpClient) { }

getAllEventos(): Observable<Evento[]>{
  return this.http.get<Evento[]>(this.baseUrl);
}

getEventosByTema(tema: string): Observable<Evento[]>{
  return this.http.get<Evento[]>(`${this.baseUrl}/bytema/${tema}`);
}

getEventosById(id: number): Observable<Evento>{
  return this.http.get<Evento>(`${this.baseUrl}/${id}`);
}

}
