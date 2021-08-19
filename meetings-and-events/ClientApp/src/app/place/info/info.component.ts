import { Component, Inject} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {PlaceTile} from "../tile/tile.component";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-place-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class PlaceInfoComponent {
  placeinfo: PlaceInfo;

  private baseUrl: string;
  private http: HttpClient;

  constructor(private route: ActivatedRoute, @Inject('BASE_URL') baseUrl: string, http: HttpClient) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  ngOnInit() {
    var id = this.route.snapshot.paramMap.get('id');
    this.http.get<PlaceInfo>(this.baseUrl + 'place/placeinfo?id=' + id).subscribe(result => {
      this.placeinfo = result;
    }, error => console.error(error));
  }
}

class PlaceInfo {
  id_place: number;
  title: string;
  description: string;
  image: string;
  users_username: string;
  address_city: string;
  rate_likes: number;
  rate_dislikes: number;
  multitime: boolean;
}
