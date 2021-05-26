import {Component, Inject, OnInit} from '@angular/core';
import { SharedService } from './../../nav-change.service';
import { HttpClient } from "@angular/common/http";
import { CookieService } from "ngx-cookie-service";
import { Router } from "@angular/router";
import {NgForm} from "@angular/forms";
import {PlaceTile} from "../../place/tile/tile.component";

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SignInComponent implements OnInit {
    invalidLogin: boolean = false;
    public errorMessage: string;

    private http: HttpClient;
    private baseUrl: string;

    constructor(private sharedService: SharedService, private router: Router, private cookieService: CookieService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    ngOnInit() {
    }

    login(form: NgForm) {
        const credentials = {
            'email': form.value.email,
            'password': form.value.password
        }

        this.http.post(this.baseUrl + "account/login", credentials)
            .subscribe(response => {
                const token = (<any>response).token;
                localStorage.setItem("jwt", token);
                this.invalidLogin = false;

                this.loggedUser(token);

                this.router.navigateByUrl("/");
            }, error => {
                this.errorMessage = error.error;
                this.invalidLogin = true;
            })
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
