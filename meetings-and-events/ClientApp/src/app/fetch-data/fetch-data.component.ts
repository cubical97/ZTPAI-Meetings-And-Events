import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public events: Event[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Event[]>(baseUrl + 'event').subscribe(result => {
      this.events = result;
    }, error => console.error(error));
  }
}

interface comments {
  author_id: number;
  comment: string;
}

interface Event {
  title: string;
  image: string;
  date_start: string;
  date_end: string;
  description: string;
  address: string;
  address_2: string;
  country: string;
  state: string;
  zip: string;
  stars_1: number;
  stars_2: number;
  stars_3: number;
  stars_4: number;
  stars_5: number;
  stars_summary: number;
  comments: comments[];
}
