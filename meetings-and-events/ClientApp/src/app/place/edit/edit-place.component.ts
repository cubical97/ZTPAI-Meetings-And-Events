﻿import { Component, Inject } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {NgForm} from "@angular/forms";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-edit-place',
  templateUrl: './edit-place.component.html',
  styleUrls: ['./edit-place.component.css']
})
export class EditPlaceComponent {
  switch_expression: string = "default";
  match_expression_1: string = "one";
  match_expression_2: string = "multi";

  placeId: string;
  placeInfo: PlaceInfo;

  private old_title: string;
  private old_description: string;
  private old_address: string;

  private new_title: string;
  private new_description: string;

  private baseUrl: string;
  private http: HttpClient;

  constructor(private router: Router, private route: ActivatedRoute, @Inject('BASE_URL') baseUrl: string,
              http: HttpClient) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  ngOnInit() {
    this.old_title = 'testowy tytul';
    this.old_description = 'testowy opis jest długi';
    this.old_address = "Aberta Zyruga Afka 41";

    this.placeId = this.route.snapshot.paramMap.get('id');
    this.loadInfo();
  }

  loadInfo() {
    this.http.get<PlaceInfo>(this.baseUrl + 'place/placeinfo?id=' + this.placeId).subscribe(result => {
      this.placeInfo = result;
      this.old_title = this.placeInfo.title;
      this.old_address = this.placeInfo.address_city;
      this.old_description = this.placeInfo.description;
    }, error => console.error(error));
  }

  onKeyTitle(event: any) {
    this.new_title = event.target.value;
  }

  onKeyDescription(event: any) {
    this.new_description = event.target.value;
  }

  changename(form: NgForm) {
    if (this.new_title)
      this.old_title = this.new_title;
    if (this.new_description)
      this.old_description = this.new_description;

    if (!this.new_title || this.new_title.length < 1)
      return;
    if (!this.new_description || this.new_description.length < 1)
      this.new_description = null;

    const credentials = {
      'place_id': parseInt(this.placeId),
      'title': this.new_title,
      'description': this.new_description
    }

    this.http.post(this.baseUrl + "placeedit/edittitle", credentials)
        .subscribe(response => {
        }, error => {
          console.error(error.error);
        })
  }
  deleteplace() {
    const credentials= {
      'place_id': Number(this.placeId)
    }
    
    this.http.post(this.baseUrl + "placeedit/removeplace", credentials)
        .subscribe(response => {
          this.router.navigateByUrl("/myplaces");
        }, error => {
          console.error(error.error);
        })
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
  own: boolean;
}
