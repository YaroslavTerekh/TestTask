import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment.prod';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UploadsService {

  constructor(
    private readonly httpClient: HttpClient
  ) { }

  public uploadFile(request: FormData): Observable<any> {
    return this.httpClient.post(`${environment.apiAddress}/Uploads/upload`, request);
  }
}
