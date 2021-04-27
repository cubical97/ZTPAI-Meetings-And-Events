import {Component, OnInit} from '@angular/core';
import { SharedService } from './../../nav-change.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SignInComponent implements OnInit {
  constructor(private sharedService:SharedService) { }
  ngOnInit() {
  }
  logged_bar(){
    this.sharedService.sendClickEvent();
  }
}
