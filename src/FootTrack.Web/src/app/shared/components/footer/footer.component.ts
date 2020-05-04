import { Subscription } from 'rxjs';
import { HidingService } from '../../../core/services/hiding.service';
import { Component, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.less'],
})
export class FooterComponent implements OnInit, OnDestroy {
  private subscription: Subscription;
  public isHidden = true;

  constructor(private hidingService: HidingService) {}

  ngOnInit(): void {
    this.subscription = this.hidingService
      .getAlert()
      .subscribe((arg: boolean) => (this.isHidden = arg));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
