import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ageValidator } from 'src/app/_validators/age.validator';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  editForm: FormGroup;
  user: User;

  constructor(private route: ActivatedRoute, private userService: UserService,
              private authService: AuthService, private alertifyService: AlertifyService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data.user;
    });

    this.initEditForm();
  }

  editUserData() {
    if (this.editForm.dirty) {
      const inputDate = new Date(this.editForm.value.dateOfBirth);
      const utcDate = Date.UTC(inputDate.getFullYear(), inputDate.getMonth(), inputDate.getDate());
      this.editForm.value.dateOfBirth = new Date(utcDate);

      this.userService.updateUser(
        this.authService.decodedToken.nameid,
        this.editForm.value).subscribe(next => {
          this.alertifyService.success('Information updeted successfully');
          this.editForm.reset(this.editForm.value);
        }, error => {
          this.alertifyService.error(error);
        });
    }
  }

  initEditForm() {
    this.editForm = new FormGroup({
      dateOfBirth: new FormControl(this.user.dateOfBirth, [
        Validators.required,
        ageValidator(18)]),
      gender: new FormControl(this.user.gender, [
        Validators.maxLength(32)
      ]),
      city: new FormControl(this.user.city, [
        Validators.maxLength(32)
      ]),
      country: new FormControl(this.user.country, [
        Validators.maxLength(32)
      ]),
      status: new FormControl(this.user.status, [
        Validators.maxLength(256)
      ]),
      activities: new FormControl(this.user.activities, [
        Validators.maxLength(256)
      ]),
      favoriteMovies: new FormControl(this.user.favoriteMovies, [
        Validators.maxLength(256)
      ]),
      favoriteGames: new FormControl(this.user.favoriteGames, [
        Validators.maxLength(256)
      ]),
      favoriteQuotes: new FormControl(this.user.favoriteQuotes, [
        Validators.maxLength(256)
      ])
    });
  }
}
