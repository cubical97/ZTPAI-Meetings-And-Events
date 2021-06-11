import {Component, Inject} from '@angular/core';
import {NgForm} from "@angular/forms";
import {SharedService} from "../../nav-change.service";
import {Router} from "@angular/router";
import {CookieService} from "ngx-cookie-service";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-user-options',
  templateUrl: './user-options.component.html',
  styleUrls: ['./user-options.component.css']
})
export class UserOptionsComponent {
  invalidUpdate: boolean;
  invalidDelete: boolean;
  deleteMode: boolean;
  errorMessage: string;

  private http: HttpClient;
  private baseUrl: string;

  constructor(private sharedService: SharedService, private router: Router, private cookieService: CookieService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.errorMessage = "";
  }

  changepass(form: NgForm) {
    const credentials = {
      'password1': form.value.password1,
      'password2': form.value.password2,
      'password3': form.value.password3
    }
    this.invalidUpdate = false;
    this.invalidDelete = false;
    
    this.http.post(this.baseUrl + "account/changepass", credentials)
        .subscribe(response => {
          this.router.navigateByUrl("/options");
        }, error => {
          this.errorMessage = error.error;
          this.invalidUpdate = true;
        })
  }

  deletemeunlock() {
    this.invalidUpdate = false;
    this.invalidDelete = false;

    this.deleteMode = true;
  }

  deleteme(form: NgForm) {
    const credentials = {
      'email': form.value.email,
      'password': form.value.password
    }

    this.http.post(this.baseUrl + "account/delete", credentials)
        .subscribe(response => {
          this.router.navigateByUrl("/logout");
        }, error => {
          this.errorMessage = error.error;
          this.invalidDelete = true;
        })
  }
}
