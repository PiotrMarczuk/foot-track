import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { AlertService } from '../../services/alert.service';

@Component({
  selector: 'app-alert-dismissable',
  templateUrl: './alert-dismissable.component.html',
})
export class AlertDismissableComponent implements OnInit, OnDestroy {
  private subscription: Subscription;
  message: any;
  dissmissible = true;
  timeout = 3000;

  constructor(private alertService: AlertService) {}
  ngOnInit(): void {
    this.subscription = this.alertService.getAlert().subscribe((message) => {
      this.scrollToTop();
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
