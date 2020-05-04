import { Component, OnInit } from '@angular/core';
import { HidingService } from 'src/app/core/services/hiding.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.less'],
})
export class MainPageComponent implements OnInit {
  constructor(private hidingService: HidingService) {}
  ngOnInit(): void {
    this.hidingService.hideComponents(false);
  }
}
