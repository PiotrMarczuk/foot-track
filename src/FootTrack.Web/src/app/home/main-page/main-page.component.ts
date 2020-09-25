import { Component, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { HidingService } from 'src/app/core/services/hiding.service';
import { Message } from '../message';
import {Id} from '../id';
import { environment } from '../../../environments/environment';
import { AuthenticationService } from '../../core/services/authentication.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.less'],
})
export class MainPageComponent implements OnInit {
  private message$: Subject<Message>;
  private connection: HubConnection;

  constructor(
    private hidingService: HidingService,
    private http: HttpClient,
    private authenticationService: AuthenticationService,
) {
    this.message$ = new Subject<Message>();
    this.connection = new HubConnectionBuilder()
    .withUrl(environment.hubUrl)
    .build();

    this.start();
  }

  ngOnInit(): void {
    this.hidingService.hideComponents(false);
  }

  private start() {
    this.connection.start().catch(err => console.log(err));

    this.connection.on('SendMessage', (message) => {
      console.log(message);
      this.message$.next(message);
    });
  }

  public sendRequest() {
    console.log(this.authenticationService.currentUserValue.email);
    console.log(this.authenticationService.currentUserValue.id);

    this.http.post<Id>(`${environment.apiUrl}/trainings/start`,
    this.authenticationService.currentUserValue.id).subscribe(
      () => {
        console.log('success');
      },
      (error) => {
        console.log('error');
      }
    );
  }

  private end() {
    this.connection.stop();
  }
}
