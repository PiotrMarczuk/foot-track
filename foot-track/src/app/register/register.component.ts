import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user.model';
import { Router } from '@angular/router';
import { AuthenticationService } from '../_services/authentication.service';
import { UserService } from '../_services/user.service';
import { AlertService } from '../_services/alert.service';
import { NgForm } from '@angular/forms';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.less']
})
export class RegisterComponent {

  loading = false;
  submitted = false;

  userData = new User();
  public passwordRetype: string;

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
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (form.invalid) {
      return;
    }

    console.log(form.value);

    this.loading = true;
    this.userService.register(form.value)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Registration successful', true);
          this.router.navigate(['/login']);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }

}
