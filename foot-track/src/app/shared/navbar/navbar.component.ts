import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { HidingService } from 'src/app/core/services/hiding.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.less']
})
export class NavbarComponent implements OnInit, OnDestroy {

  private subscription: Subscription;
  public isHidden = true;

  constructor(private hidingService: HidingService) { }

  ngOnInit(): void {
    this.subscription = this.hidingService.getAlert()
      .subscribe(arg => this.isHidden = arg);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
