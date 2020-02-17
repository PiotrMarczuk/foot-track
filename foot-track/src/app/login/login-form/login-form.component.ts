import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from 'src/app/_models/user.model';
import { AlertService } from 'src/app/_services/alert.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.less']
})
export class LoginFormComponent {
  private returnUrl = '/';
  userData = new User();

  constructor(
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private router: Router,
  ) {
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/']);
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
