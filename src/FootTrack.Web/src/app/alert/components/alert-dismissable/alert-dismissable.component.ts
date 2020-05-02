import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Subscription } from 'rxjs';
import { AlertService } from '../../services/alert.service';
import { Alert } from '../../models/alert';
import { Router, NavigationStart } from '@angular/router';

@Component({
  selector: 'app-alert-dismissable',
  templateUrl: './alert-dismissable.component.html',
})
export class AlertDismissableComponent implements OnInit, OnDestroy {
  @Input() id = 'default-alert';
  alerts: Alert[] = [];
  alertSubscription: Subscription = Subscription.EMPTY;
  routeSubscription: Subscription = Subscription.EMPTY;

  dissmissible = true;

  constructor(private router: Router, private alertService: AlertService) {}
  ngOnInit(): void {
    this.alertSubscription = this.alertService.onAlert(this.id)
      .subscribe(alert => {
        if (!alert.message) {
          this.alerts = this.alerts.filter(x => x.keepAfterRouteChange);

          this.alerts.forEach(x => delete x.keepAfterRouteChange);
          return;
        }

        this.alerts.push(alert);
      });

    this.routeSubscription = this.router.events.subscribe(event => {
        if (event instanceof NavigationStart) {
            this.alertService.clear(this.id);
        }
    });
  }

  ngOnDestroy() {
    this.alertSubscription.unsubscribe();
    this.routeSubscription.unsubscribe();
  }

  removeAlert(alert: Alert) {
    if (!this.alerts.includes(alert)) { return; }
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
