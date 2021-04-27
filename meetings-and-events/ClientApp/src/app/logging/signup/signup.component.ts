import { Component, OnInit } from '@angular/core';
import { SharedService } from './../../nav-change.service';

@Component({
  selector: 'app-singup',
  templateUrl: './signup.component.html',
  styleUrls: ['signup.component.css']
})
export class SignUpComponent implements OnInit {
  constructor(private sharedService:SharedService) { }
  ngOnInit() {
  }
  logged_bar(){
    this.sharedService.sendClickEvent();
  }
}
