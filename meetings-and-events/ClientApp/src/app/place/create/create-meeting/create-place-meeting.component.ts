import { Component } from '@angular/core';
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-create-place-meeting',
  templateUrl: './create-place-meeting.component.html',
  styleUrls: ['./create-place-meeting.component.css']
})
export class CreatePlaceMeetingComponent {
  errorMessage: string;

  create(form: NgForm) {
    var description;

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

    if (!form.value.dp) {
      this.errorMessage = "Date not set";
      return;
    }

    if (!form.value.timeOpen || !form.value.timeClose) {
      this.errorMessage = "Time not set";
      return;
    }
    if (form.value.timeOpen >= form.value.timeClose) {
      this.errorMessage = "Close time may be after open time";
      return;
    }


    const credentials = {
      'country': form.value.country,
      'city': form.value.city,
      'street': form.value.street,
      'number': form.value.number,
      'description': description,
      'datepicker': form.value.dp,
      'timeOC': [form.value.timeOpen, form.value.timeClose]
    }

    // TODO upload
    console.error(credentials);
  }
}