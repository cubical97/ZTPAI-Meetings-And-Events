import {Component, Inject, OnInit} from '@angular/core';
import { SharedService } from './../../nav-change.service';
import { HttpClient } from "@angular/common/http";
import { CookieService } from "ngx-cookie-service";
import { Router } from "@angular/router";
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-singup',
  templateUrl: './signup.component.html',
  styleUrls: ['signup.component.css']
})
export class SignUpComponent implements OnInit {
  invalidRegister: boolean = false;
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

  register(form: NgForm) {


    const credentials = {
      'username': form.value.name,
      'email': form.value.email,
      'password': form.value.inputPassword
    }

    if (this.validateRegister(form.value.name, form.value.email, form.value.inputPassword, form.value.inputPassword2))
      this.http.post(this.baseUrl + "account/register", credentials)
          .subscribe(response => {
            const token = (<any>response).token;
            localStorage.setItem("jwt", token);
            this.invalidRegister = false;

            this.loggedUser(token);

            this.router.navigateByUrl("/");
          }, error => {
            this.errorMessage = error.error;
            this.invalidRegister = true;
          })
  }

  validateRegister(name: string, mail: string, pass1: string, pass2: string): boolean {
    if (!name) {
      this.errorMessage = "Set username!";
      this.invalidRegister = true;
      return false;
    }
    if (!mail) {
      this.errorMessage = "Set email!";
      this.invalidRegister = true;
      return false;
    }
    if (pass1.length < 8) {
      this.errorMessage = "Password must contain at least 8 characters!";
      this.invalidRegister = true;
      return false;
    }
    if (pass1 != pass2) {
      this.errorMessage = "Password must be the same!"
      this.invalidRegister = true;
      return false;
    }

    this.invalidRegister = false;
    return true;
  }

  loggedUser(token) {
    if (token) {

      this.http.get<string>(this.baseUrl + "account/getusername")
          .subscribe(result => {
            this.cookieService.set('meetings-and-events-logged', "true");
            this.cookieService.set('meetings-and-events-username', result);
            this.sharedService.sendClickEvent();
          }, error => console.error(error));
    }
  }
}
