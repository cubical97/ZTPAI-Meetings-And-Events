import { Component } from '@angular/core';
import {NgForm} from "@angular/forms";
import {ValueConverter} from "@angular/compiler/src/render3/view/template";

@Component({
  selector: 'app-create-place-meeting',
  templateUrl: './create-place-meeting.component.html',
  styleUrls: ['./create-place-meeting.component.css']
})
export class CreatePlaceMeetingComponent {
  errorMessage: string;

  create(form: NgForm) {
    const credentials = {
      'country': form.value.country,
      'city': form.value.city,
      'street': form.value.street,
      'number': form.value.number,
      'description': form.value.description,
      'datepicker': form.value.dp,
      'timeOC': [form.value.timeOpen, form.value.timeClose]
    }
    
    // TODO validation
    
    // TODO upload
    console.error(credentials);
  }
}