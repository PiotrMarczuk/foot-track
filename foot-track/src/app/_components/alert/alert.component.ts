import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { AlertService } from 'src/app/_services/alert.service';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
})
export class AlertComponent implements OnInit, OnDestroy {
  private subscription: Subscription;
  message: any;

  constructor(private alertService: AlertService) { }

  ngOnInit(): void {
    this.subscription = this.alertService.getAlert().subscribe(message => {
      switch (message && message.type) {
        case 'success':
          message.cssClass = 'alert alert-success alert-dismissible fade show';
          break;
        case 'error':
          message.cssClass = 'alert alert-danger alert-dismissible fade show';
          break;
      }
      this.message = message;
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  closeAlert() {
    this.message = null;
  }

}
