import { Component, OnInit } from '@angular/core';
import { UserForRegister } from '../_models/user-for-register';

import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { ageValidator } from '../_validators/age.validator';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: UserForRegister;
  registerForm: FormGroup;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) { }

  ngOnInit() {
    this.initForm();
  }

  register() {
    if (this.registerForm.valid) {
      this.formatDateToUtc();

      this.authService.register(this.registerForm.value).subscribe(() => {
        this.alertify.success('Success registration');
        this.router.navigate(['login']);
      }, error => {
        this.alertify.error(error);
      });
    }
  }

  private initForm() {
    this.registerForm = new FormGroup({
      userName: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(16)]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(20)]),
      name: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(30)]),
      surname: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(30)]),
      dateOfBirth: new FormControl('', [
        Validators.required,
        ageValidator(18)])
    });
  }

  formatDateToUtc() {
    const inputDate = new Date(this.registerForm.value.dateOfBirth);
    const utcDate = Date.UTC(inputDate.getFullYear(), inputDate.getMonth(), inputDate.getDate());
    this.registerForm.value.dateOfBirth = new Date(utcDate);
  }
}
