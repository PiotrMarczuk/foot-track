import { Component, OnInit, OnDestroy } from '@angular/core';
import { HidingService } from 'src/app/core/services/hiding.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.less']
})
export class FooterComponent implements OnInit, OnDestroy {
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
