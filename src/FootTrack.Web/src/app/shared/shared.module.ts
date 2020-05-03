import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [FooterComponent, NavbarComponent],
  imports: [CommonModule, NgbModule],
  exports: [FooterComponent, NavbarComponent]
})
export class SharedModule {}
