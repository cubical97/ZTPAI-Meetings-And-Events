import {Component, Inject} from '@angular/core';
import {PlaceTile} from "../../place/tile/tile.component";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-user-list-join',
  templateUrl: './user-list-join.component.html',
  styleUrls: ['./user-list-join.component.css']
})
export class UserListJoinComponent {
  places_list: PlaceTile[];

  private http: HttpClient;
  private baseUrl: string;

  constructor(private router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.http.get<PlaceTile[]>(this.baseUrl + 'place/listjoined').subscribe(result => {
      this.places_list = result;
    }, error => console.error(error));
  }
}
