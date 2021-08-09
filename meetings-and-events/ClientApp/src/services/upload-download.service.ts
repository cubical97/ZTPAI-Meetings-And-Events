import {Inject, Injectable} from '@angular/core';
import { HttpClient, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class UploadDownloadService {
  private baseApiUrl: string;
  private apiUploadUrl: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseApiUrl = baseUrl + 'image/';
    this.apiUploadUrl = this.baseApiUrl + 'upload';
  }

  public uploadFile(file: Blob): Observable<HttpEvent<void>> {
    const formData = new FormData();
    formData.append('file', file);

    return this.httpClient.request(new HttpRequest(
      'POST',
      this.apiUploadUrl,
      formData,
      {
        reportProgress: true
      }));
  }
}
