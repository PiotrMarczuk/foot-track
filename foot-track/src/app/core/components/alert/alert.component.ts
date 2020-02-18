import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { AlertService } from '../../services/alert.service';

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
      this.scrollToTop();
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

  scrollToTop() {
    const scrollToTop = window.setInterval(() => {
      const pos = window.pageYOffset;
      if (pos > 0) {
        window.scrollTo(0, pos - 5);
      } else {
        window.clearInterval(scrollToTop);
      }
    }, 16);
  }

}
