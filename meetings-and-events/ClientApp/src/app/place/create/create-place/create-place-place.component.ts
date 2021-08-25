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

  private good_request: boolean;

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

    this.good_request = true;

    this.errorMessage = "";
    if (!form.value.title || form.value.country.title < 1) {
      this.errorMessage = "Title too short\n";
      this.good_request = false;
    }
    if (!form.value.country || form.value.country.length < 1) {
      this.errorMessage += "Country name too short\n";
      this.good_request = false;
    }
    if (!form.value.city || form.value.city.length < 1) {
      this.errorMessage += "City name too short\n";
      this.good_request = false;
    }
    if (!form.value.street || form.value.street.length < 1) {
      this.errorMessage += "Street name too short\n";
      this.good_request = false;
    }
    if (!form.value.number || form.value.number.length < 1) {
      this.errorMessage += "Number name too short\n";
      this.good_request = false;
    }
    if (!form.value.description || form.value.description.length < 1)
      description = null;
    else
      description = form.value.description;

    timeday1 = this.checkTime(form.value.time1check, form.value.time1Open, form.value.time1Close);
    timeday2 = this.checkTime(form.value.time2check, form.value.time2Open, form.value.time2Close);
    timeday3 = this.checkTime(form.value.time3check, form.value.time3Open, form.value.time3Close);
    timeday4 = this.checkTime(form.value.time4check, form.value.time4Open, form.value.time4Close);
    timeday5 = this.checkTime(form.value.time5check, form.value.time5Open, form.value.time5Close);
    timeday6 = this.checkTime(form.value.time6check, form.value.time6Open, form.value.time6Close);
    timeday7 = this.checkTime(form.value.time7check, form.value.time7Open, form.value.time7Close);

    if (!(timeday1 || timeday2 || timeday3 || timeday4 || timeday5 || timeday6 || timeday7)) {
      this.errorMessage += "No time set\n";
      this.good_request = false;
    }

    if (!this.good_request)
      return;

    const credentials = {
      'title': form.value.title,
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

    this.http.post(this.baseUrl + "place/createplace", credentials)
        .subscribe(response => {
          this.router.navigateByUrl("/myplaces");
        }, error => {
          this.errorMessage = error.error;
        })
    //TODO show error or go
  }

  checkTime(checkbox, time1, time2) {
    if (checkbox) {
      if (time1 && time2) {
        if (time1 < time2)
          return [time1, time2];
        else {
          this.errorMessage += "Close time must be after open time\n";
        }
      } else {
        this.errorMessage = "Not every time is set";
        this.good_request = false;
      }
    }
    return null;
  }
}
