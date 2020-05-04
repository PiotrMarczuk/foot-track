import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertDismissableComponent } from './components/alert-dismissable/alert-dismissable.component';
import { AlertModule } from 'ngx-bootstrap/alert';

@NgModule({
  declarations: [AlertDismissableComponent],
  imports: [AlertModule.forRoot(), CommonModule],
  exports: [AlertDismissableComponent],
})
export class AlertingModule {}
