import { Component, OnInit } from '@angular/core';
import { UserForRegister } from '../_models/UserForRegister';

import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: UserForRegister;
  registerForm: FormGroup;

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required]),
      surname: new FormControl('', [Validators.required]),
      dateOfBirth: new FormControl('', [Validators.required])
    });
  }

  register() {
    this.authService.register(this.registerForm.value).subscribe(() => {
      this.alertify.success('Success registration');
    }, error => {
      this.alertify.error(error);
    });
  }
}
