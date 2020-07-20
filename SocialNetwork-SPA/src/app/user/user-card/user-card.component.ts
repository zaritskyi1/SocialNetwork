import { Component, OnInit, Input } from '@angular/core';
import { UserForList } from 'src/app/_models/userForList';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent implements OnInit {
  @Input()
  user: UserForList;

  constructor() { }

  ngOnInit() {
  }

}
