import { Component, Inject, OnInit } from '@angular/core';
import { PlaceTile } from "../place/tile/tile.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['home.component.css']
})
export class HomeComponent {
  places_list: PlaceTile[];

  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.http.get<PlaceTile[]>(this.baseUrl + 'place/list').subscribe(result => {
      this.places_list = result;
    }, error => console.error(error));
  }
}
