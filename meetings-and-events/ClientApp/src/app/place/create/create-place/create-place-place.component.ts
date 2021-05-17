import { Component } from '@angular/core';

@Component({
  selector: 'app-create-place-place',
  templateUrl: './create-place-place.component.html',
  styleUrls: ['./create-place-place.component.css']
})
export class CreatePlacePlaceComponent {
  inputCountry: string;
  inputCity: string;
  inputStreet: string;
  inputNumber: string;

  inputDescription: string;

  errorMessage: string;
  
  
  
}
