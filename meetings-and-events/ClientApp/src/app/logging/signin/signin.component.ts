import {Component, Inject, OnInit} from '@angular/core';
import { SharedService } from './../../nav-change.service';
import { HttpClient } from "@angular/common/http";
import { CookieService } from "ngx-cookie-service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SignInComponent implements OnInit {
  inputEmail: string;
  inputPassword: string;
  public errorMessage: string;

  private http: HttpClient;
  private baseUrl: string;

  constructor(private sharedService: SharedService, private router: Router, private cookieService: CookieService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.errorMessage = "";
  }

  ngOnInit() {
  }

  logged_bar() {
    if (this.inputEmail && this.inputPassword) {
      // TODO add input validation

      this.http.get<LoginBlock>(this.baseUrl + 'accountlogin/Get?password=' + this.inputPassword + '&email=' + this.inputEmail).subscribe(result => {
        if (result.logged) {
          this.cookieService.set('meetings-and-events-logged', "true");
          this.cookieService.set('meetings-and-events-iduser', result.idUser.toString());
          this.cookieService.set('meetings-and-events-username', result.username);

          this.sharedService.sendClickEvent();
          this.router.navigate(['/']);
        } else {
          this.errorMessage = result.errorMessage;
        }
      }, error => console.error(error));
    }
  }
}

interface LoginBlock {
  logged: boolean;
  errorMessage: string;
  idUser: number;
  username: string;
}
