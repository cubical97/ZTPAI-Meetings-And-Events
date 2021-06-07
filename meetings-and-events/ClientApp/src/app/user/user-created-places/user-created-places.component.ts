import {Component, Inject} from '@angular/core';
import {PlaceTile} from "../../place/tile/tile.component";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-user-created-places',
  templateUrl: './user-created-places.component.html',
  styleUrls: ['./user-created-places.component.css']
})
export class UserCreatedPlacesComponent {
  places_list: PlaceTile[];

  private http: HttpClient;
  private baseUrl: string;

  constructor(private router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.http.get<PlaceTile[]>(this.baseUrl + 'place/listuserown').subscribe(result => {
      this.places_list = result;
    }, error => console.error(error));
  }
}
