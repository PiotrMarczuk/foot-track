import {
  Component,
  Input,
  TemplateRef,
  OnInit,
  OnDestroy,
} from '@angular/core';
import { HidingService } from 'src/app/core/services/hiding.service';

@Component({
  selector: 'app-form-template',
  templateUrl: './form-template.component.html',
  styleUrls: ['./form-template.component.less'],
})
export class FormTemplateComponent implements OnInit, OnDestroy {
  @Input() itemTemplate: TemplateRef<any>;

  constructor(private hidingService: HidingService) {}

  ngOnInit() {
    this.hidingService.hideComponents();
  }

  ngOnDestroy() {
    this.hidingService.hideComponents(false);
  }
}
