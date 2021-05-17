import { Component } from '@angular/core';

@Component({
  selector: 'app-create-place-meeting',
  templateUrl: './create-place-meeting.component.html',
  styleUrls: ['./create-place-meeting.component.css']
})
export class CreatePlaceMeetingComponent {
  model;

  inputCountry: string;
  inputCity: string;
  inputStreet: string;
  inputNumber: string;

  inputDescription: string;

  errorMessage: string;
  
  
  
}
