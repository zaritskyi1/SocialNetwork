import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

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

    this.editForm = new FormGroup({
      birthDate: new FormControl(this.user.dateOfBirth),
      gender: new FormControl(this.user.gender),
      city: new FormControl(this.user.city),
      country: new FormControl(this.user.country),
      status: new FormControl(this.user.status),
      activities: new FormControl(this.user.activities),
      favoriteMovies: new FormControl(this.user.favoriteMovies),
      favoriteGames: new FormControl(this.user.favoriteGames),
      favoriteQuotes: new FormControl(this.user.favoriteQuotes)
    });
  }

  editUserData() {
    if (this.editForm.dirty) {
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
}
