import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { first } from 'rxjs/operators';
import { User } from 'src/app/core/models/user.model';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { UserService } from 'src/app/core/services/user.service';
import { AlertService } from 'src/app/core/services/alert.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.less']
})
export class RegisterComponent {
  userData = new User();
  model: any = {};

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private userService: UserService,
    private alertService: AlertService
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

    if (this.userData.firstName === null) {
      this.userData.firstName = '';
    }
    if (this.userData.lastName === null) {
      this.userData.lastName = '';
    }

    this.userService.register(form.value)
      .pipe(first())
      .subscribe(
        () => {
          this.alertService.success('Registration successful', true);
          this.router.navigate(['identify/login']);
        },
        error => {
          this.alertService.error(error);
        });
  }

}
