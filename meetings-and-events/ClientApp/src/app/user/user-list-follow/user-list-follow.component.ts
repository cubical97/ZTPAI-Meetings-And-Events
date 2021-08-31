import {Component, Inject} from '@angular/core';
import {PlaceTile} from "../../place/tile/tile.component";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-user-list-follow',
  templateUrl: './user-list-follow.component.html',
  styleUrls: ['./user-list-follow.component.css']
})
export class UserListFollowComponent {
  places_list: PlaceTile[];

  private http: HttpClient;
  private baseUrl: string;

  constructor(private router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.http.get<PlaceTile[]>(this.baseUrl + 'place/listfollowed').subscribe(result => {
      this.places_list = result;
    }, error => console.error(error));
  }
}
