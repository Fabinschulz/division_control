import { Component, Input, OnInit, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { AbstractControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';

@Component({
  selector: 'app-form-input',
  templateUrl: './form-input.component.html',
  styleUrls: ['./form-input.component.scss'],
})
export class FormInputComponent implements OnInit {
  @Output() customActionsEmitter: EventEmitter<void> = new EventEmitter();

  @Output() onBlurEmitter: EventEmitter<string> = new EventEmitter<string>();

  @Input() parentFormGroup: any;

  @Input() showName: string;

  @Input() name: string;

  @Input() hasMask: string;

  @Input() hasCurrencyMask = false;

  @Input() helpText = '';

  @Input() placeholder = '';

  @Input() inputType = 'text';

  @Input() buttonText = false;

  @Input() label: string;

  public switchCase: string;

  constructor() {}

  ngOnInit() {
    if (this.hasMask) {
      this.switchCase = 'mask';
    } else if (this.hasCurrencyMask) {
      this.switchCase = 'currency';
    } else {
      this.switchCase = 'text';
    }
  }

  handleOnBlur(event: FocusEvent): void {
    const value = this.parentFormGroup.get(this.name).value;
    this.onBlurEmitter.emit(value);
  }

  get hasErrors(): boolean {
    return (
      this.hasRequiredError &&
      this.hasMinimumLengthError &&
      this.hasMaximumLengthError &&
      this.hasEmailError &&
      this.hasMinimumError &&
      this.hasMaximumError
    );
  }

  get fieldErrors(): ValidationErrors {
    return this.parentFormGroup.get(this.name).errors;
  }

  get fieldControl(): AbstractControl {
    return this.parentFormGroup.get(this.name);
  }

  get hasRequiredError(): boolean {
    return this.parentFormGroup.get(this.name).hasError('required');
  }

  get hasDocumentError(): boolean {
    return this.parentFormGroup.get(this.name).hasError('document');
  }

  get hasMinimumLengthError(): boolean {
    return this.parentFormGroup.get(this.name).hasError('minlength');
  }

  get hasMaximumLengthError(): boolean {
    return this.parentFormGroup.get(this.name).hasError('maxlength');
  }

  get hasEmailError(): boolean {
    return this.parentFormGroup.get(this.name).hasError('email');
  }

  get hasMinimumError(): boolean {
    return this.parentFormGroup.get(this.name).hasError('min');
  }

  get minimumValue(): string {
    return this.parentFormGroup.get(this.name).errors.min.min;
  }

  get hasMaximumError(): boolean {
    return this.parentFormGroup.get(this.name).hasError('max');
  }

  get maximumValue(): string {
    return this.parentFormGroup.get(this.name).errors.max.max;
  }

  get hasPercentageError(): boolean {
    return this.parentFormGroup.get(this.name).hasError('invalid_percentage');
  }

  get isRequired(): boolean {
    const abstractControl: AbstractControl = this.parentFormGroup['controls'][this.name];
    return this.hasRequiredField(abstractControl);
  }

  hasRequiredField = (abstractControl: any): boolean => {
    if (abstractControl.validator) {
      const validator = abstractControl.validator({} as AbstractControl);
      if (validator && validator['required']) {
        return true;
      }
    }
    if (abstractControl['controls']) {
      for (const controlName in abstractControl['controls']) {
        if (abstractControl['controls'][controlName]) {
          if (this.hasRequiredField(abstractControl['controls'][controlName])) {
            return true;
          }
        }
      }
    }
    return false;
  };
}
