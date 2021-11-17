import { Directive, Input } from '@angular/core';
import { NG_VALIDATORS, Validator, ValidationErrors, FormGroup } from '@angular/forms';

import { BothMatch } from '../both-match.validator';

@Directive({
  selector: '[bothMatch]',
  providers: [{ provide: NG_VALIDATORS, useExisting: BothMatchDirective, multi: true }]
})
export class BothMatchDirective implements Validator {

  @Input('bothMatch') bothMatch: string[] = [];

  validate(formGroup: FormGroup): ValidationErrors {
    return BothMatch(this.bothMatch[0], this.bothMatch[1])(formGroup) ?? <ValidationErrors>{};
  }
}
