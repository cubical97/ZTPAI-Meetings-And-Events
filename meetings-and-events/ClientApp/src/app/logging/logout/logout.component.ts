import {Component, Inject} from '@angular/core';
import {CookieService} from "ngx-cookie-service";
import {Router} from "@angular/router";
import {SharedService} from "../../nav-change.service";

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html'
})
export class LogOutComponent {

  constructor(private sharedService: SharedService, private router: Router, private cookieService: CookieService) {
  }

  public ngOnInit(): void {
    this.cookieService.set('meetings-and-events-logged', "false");
    this.cookieService.set('meetings-and-events-iduser', "-1");
    this.cookieService.set('meetings-and-events-username', "unlogged");

    this.sharedService.sendClickEvent();
    this.router.navigate(['/']);
  }

}
