import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/core/models/user.model';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { AlertService } from 'src/app/alert/services/alert.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less']
})
export class LoginComponent {
  private returnUrl = '/';
  userData = new User();

  constructor(
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private router: Router,
  ) {
    if (this.authenticationService.currentUserValue) {
      this.router.navigate([this.returnUrl]);
    }
  }

  onSubmit(form: NgForm) {
    this.alertService.clear();

    if (form.invalid) {
      return;
    }

    this.authenticationService.login(this.userData.email, this.userData.password)
      .pipe(first())
      .subscribe(
        () => {
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.alertService.error(error);
        }
      );
  }

}
