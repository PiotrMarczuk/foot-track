import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { HidingService } from '../../../core/services/hiding.service';
import { User } from '../../../core/models/user.model';
import { AuthenticationService } from '../../../core/services/authentication.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.less'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  private subscription: Subscription;
  private currentUserSubscription: Subscription;
  public isHidden = true;
  public collapsed = true;
  currentUser: User;

  constructor(
    private hidingService: HidingService,
    private authenticationService: AuthenticationService
  ) {}

  logout() {
    this.authenticationService.logout();
  }

  ngOnInit(): void {
    this.subscription = this.hidingService
      .getAlert()
      .subscribe((arg: boolean) => (this.isHidden = arg));
    this.currentUserSubscription = this.authenticationService.currentUser.subscribe(
      (x: User) => (this.currentUser = x)
    );
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    this.currentUserSubscription.unsubscribe();
  }
}
