import { Directive, Input } from '@angular/core';
import { NG_VALIDATORS, FormGroup, ValidationErrors } from '@angular/forms';
import { MustMatch } from '../validators/must-match.validator';

@Directive({
  selector: '[appMustMatch]',
  providers: [{ provide: NG_VALIDATORS, useExisting: MustMatchDirective, multi: true }]
})
export class MustMatchDirective {
  @Input() appMustMatch: string[] = [];

  validate(formGroup: FormGroup): ValidationErrors {
      return MustMatch(this.appMustMatch[0], this.appMustMatch[1])(formGroup);
  }
}
