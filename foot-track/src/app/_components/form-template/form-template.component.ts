import { Component, OnInit, Input, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-form-template',
  templateUrl: './form-template.component.html',
  styleUrls: ['./form-template.component.less']
})
export class FormTemplateComponent {
  @Input() itemTemplate: TemplateRef<any>;
}
