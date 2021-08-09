import { Component } from '@angular/core';
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-create-place-place',
  templateUrl: './create-place-place.component.html',
  styleUrls: ['./create-place-place.component.css']
})
export class CreatePlacePlaceComponent {
  errorMessage: string;

  create(form: NgForm) {
    const credentials = {
      'country': form.value.country,
      'city': form.value.city,
      'street': form.value.street,
      'number': form.value.number,
      'description': form.value.description,
      'timeOC1': [form.value.time1check, form.value.time1Open, form.value.time1Close],
      'timeOC2': [form.value.time2check, form.value.time2Open, form.value.time2Close],
      'timeOC3': [form.value.time3check, form.value.time3Open, form.value.time3Close],
      'timeOC4': [form.value.time4check, form.value.time4Open, form.value.time4Close],
      'timeOC5': [form.value.time5check, form.value.time5Open, form.value.time5Close],
      'timeOC6': [form.value.time6check, form.value.time6Open, form.value.time6Close],
      'timeOC7': [form.value.time7check, form.value.time7Open, form.value.time7Close]
    }

    // TODO validation

    // TODO upload
    console.error(credentials);
  }
}
