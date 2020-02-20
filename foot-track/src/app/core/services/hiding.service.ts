import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HidingService {
  private subject = new Subject<any>();

  constructor() { }

  getAlert(): Observable<any> {
    return this.subject.asObservable();
  }

  hideComponents(hide: boolean = true) {
    this.subject.next(hide);
  }
}
