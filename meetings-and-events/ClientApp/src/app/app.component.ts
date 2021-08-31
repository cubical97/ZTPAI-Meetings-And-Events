import { Component } from '@angular/core';
import { CookieService } from "ngx-cookie-service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(private cookieService: CookieService) {
  }

  public ngOnInit(): void {
    if (!this.cookieService.check('meetings-and-events-logged')) {
      this.cookieService.set('meetings-and-events-logged', "false");
      this.cookieService.set('meetings-and-events-iduser', "-1");
      this.cookieService.set('meetings-and-events-username', "unlogged");
    }
  }
}
