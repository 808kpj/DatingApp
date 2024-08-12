import { Component, inject, input, OnInit, output, runInInjectionContext } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountsService } from '../_services/accounts.service';
import { JsonPipe, NgIf } from '@angular/common';
import { TextInputComponent } from "../_forms/text-input/text-input.component";
import { DatePickerComponent } from "../_forms/date-picker/date-picker.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, TextInputComponent, DatePickerComponent, JsonPipe],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})

export class RegisterComponent implements OnInit {
  private accountService = inject(AccountsService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  registerForm: FormGroup = new FormGroup({});
  cancelRegister = output<any>();
  maxDate = new Date();
  validationErrors: string[] | undefined;

  ngOnInit(): void {
    this.initializeForm();
    //will set the year after retrieving the current year then subtracting 18 from it
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['',[Validators.required, this.matchValues('password')] ],
    });

    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return(control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {isMatching: true}
    }
  }

  register() {
    const dob = this.getDateOnly(this.registerForm.get('dateOfBirth')?.value);
    this.registerForm.patchValue({dateOfBirth: dob});

    this.accountService.register(this.registerForm.value).subscribe({
      next: response =>  this.router.navigateByUrl('/members'),
      error: error => this.validationErrors = error
    })
  }

  cancel(){
    this.cancelRegister.emit(false);
  }

  private getDateOnly(dob: string | undefined) {
    if(!dob) return;

    return new Date(dob).toISOString().slice(0, 10);
  }
}
