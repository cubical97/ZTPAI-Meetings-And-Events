import {Component, Inject} from '@angular/core';
import {NgForm} from "@angular/forms";
import {Router} from "@angular/router";
import {CookieService} from "ngx-cookie-service";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-create-place-place',
  templateUrl: './create-place-place.component.html',
  styleUrls: ['./create-place-place.component.css']
})
export class CreatePlacePlaceComponent {
  errorMessage: string;

  private http: HttpClient;
  private baseUrl: string;

  constructor(private router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  create(form: NgForm) {
    var description;
    var timeday1;
    var timeday2;
    var timeday3;
    var timeday4;
    var timeday5;
    var timeday6;
    var timeday7;

    this.errorMessage = "";
    if (!form.value.country || form.value.country.length < 1) {
      this.errorMessage = "Country name too short";
      return;
    }
    if (!form.value.city || form.value.city.length < 1) {
      this.errorMessage = "City name too short";
      return;
    }
    if (!form.value.street || form.value.street.length < 1) {
      this.errorMessage = "Street name too short";
      return;
    }
    if (!form.value.number || form.value.number.length < 1) {
      this.errorMessage = "Number name too short";
      return;
    }
    if (!form.value.description || form.value.description.length < 1)
      description = null;
    else
      description = form.value.description;

    timeday1 = this.checkTime(form.value.time1check, form.value.time1Open, form.value.time1Close);
    if (form.value.time1check && !timeday1) return;
    timeday2 = this.checkTime(form.value.time2check, form.value.time2Open, form.value.time2Close);
    if (form.value.time2check && !timeday2) return;
    timeday3 = this.checkTime(form.value.time3check, form.value.time3Open, form.value.time3Close);
    if (form.value.time3check && !timeday3) return;
    timeday4 = this.checkTime(form.value.time4check, form.value.time4Open, form.value.time4Close);
    if (form.value.time4check && !timeday4) return;
    timeday5 = this.checkTime(form.value.time5check, form.value.time5Open, form.value.time5Close);
    if (form.value.time5check && !timeday5) return;
    timeday6 = this.checkTime(form.value.time6check, form.value.time6Open, form.value.time6Close);
    if (form.value.time6check && !timeday6) return;
    timeday7 = this.checkTime(form.value.time7check, form.value.time7Open, form.value.time7Close);
    if (form.value.time7check && !timeday7) return;

    if (!(timeday1 || timeday2 || timeday3 || timeday4 || timeday5 || timeday6 || timeday7)) {
      this.errorMessage = "No time set";
      return;
    }

    const credentials = {
      'country': form.value.country,
      'city': form.value.city,
      'street': form.value.street,
      'number': form.value.number,
      'description': description,
      'timeOC1': timeday1,
      'timeOC2': timeday2,
      'timeOC3': timeday3,
      'timeOC4': timeday4,
      'timeOC5': timeday5,
      'timeOC6': timeday6,
      'timeOC7': timeday7
    }

    console.log(credentials);

    console.log(this.baseUrl);
    
    // TODO upload
    this.http.post(this.baseUrl + "place/createplace", credentials)
        .subscribe(response => {
          this.errorMessage="Send OK!";
          //TODO go to info page
          //this.router.navigateByUrl("/options");
        }, error => {
          this.errorMessage = error.error;
        })
    //TODO show error or go
  }

  checkTime(checkbox, time1, time2) {
    if (checkbox && time1 && time2) {
      if (time1 < time2)
        return [time1, time2];
      else {
        this.errorMessage = "Close time may be after open time";
      }
    }
    return null;
  }
}
