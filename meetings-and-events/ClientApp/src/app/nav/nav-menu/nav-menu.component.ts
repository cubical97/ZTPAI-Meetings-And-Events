import { Component, OnInit } from '@angular/core';
import { SharedService } from './../../nav-change.service';
import { Subscription } from 'rxjs';
import { CookieService } from "ngx-cookie-service";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  ngSwitch: any;

  clickEventsubscription: Subscription;

  public switch_expression: string = "app-nav-1";
  match_expression_1: string = "app-nav-1";
  match_expression_2: string = "app-nav-2";

  public username: string

  constructor(private sharedService: SharedService, private cookieService: CookieService) {
    this.clickEventsubscription = this.sharedService.getClickEvent().subscribe(() => {
      this.toggle_logged();
    })

    if (this.cookieService.check('meetings-and-events-logged')) {
      if (this.cookieService.get('meetings-and-events-logged') === "true") {
        this.username = this.cookieService.get('meetings-and-events-username');
        this.switch_expression = this.match_expression_2
      } else {
        this.username = null;
        this.switch_expression = this.match_expression_1
      }
    }
  }

  ngOnInit() {
    
  }

  toggle_logged() {
    if (this.cookieService.check('meetings-and-events-logged')) {
      if (this.cookieService.get('meetings-and-events-logged') === "true") {
        this.username = this.cookieService.get('meetings-and-events-username');
        this.switch_expression = this.match_expression_2
      } else {
        this.username = null;
        this.switch_expression = this.match_expression_1
      }
    }
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
