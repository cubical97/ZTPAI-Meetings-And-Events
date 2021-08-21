import {Component, Inject} from '@angular/core';
import {NgForm} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-create-place-meeting',
  templateUrl: './create-place-meeting.component.html',
  styleUrls: ['./create-place-meeting.component.css']
})
export class CreatePlaceMeetingComponent {
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

    if (!form.value.dp) {
      this.errorMessage += "Date not set\n";
      this.good_request = false;
    }

    if (!form.value.timeOpen || !form.value.timeClose) {
      this.errorMessage += "Time not set\n";
      this.good_request = false;
    } else {
      if (form.value.timeOpen >= form.value.timeClose) {
        this.errorMessage += "Close time may be after open time\n";
        this.good_request = false;
      }
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
      'datepicker': form.value.dp,
      'timeOC': [form.value.timeOpen, form.value.timeClose]
    }

    this.http.post(this.baseUrl + "place/createmeeting", credentials)
        .subscribe(response => {
          this.router.navigateByUrl("/options");
        }, error => {
          this.errorMessage = error.error;
        })
  }
}
