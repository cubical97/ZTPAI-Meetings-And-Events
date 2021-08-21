import { Component, Inject} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {CommentsComponent} from "./comments/comments.component";
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-place-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class PlaceInfoComponent {
  placeinfo: PlaceInfo;
  comment_list: CommentsComponent[];
  errorMessage: string;

  private baseUrl: string;
  private http: HttpClient;

  constructor(private route: ActivatedRoute, @Inject('BASE_URL') baseUrl: string, http: HttpClient) {
    this.baseUrl = baseUrl;
    this.http = http;

    var id = this.route.snapshot.paramMap.get('id');
    this.http.get<PlaceInfo>(this.baseUrl + 'place/placeinfo?id=' + id).subscribe(result => {
      this.placeinfo = result;
    }, error => console.error(error));

    this.http.get<CommentsComponent[]>(this.baseUrl + 'place/placeinfocomments?id=' + id).subscribe(result => {
      this.comment_list = result;
    }, error => console.error(error));
  }

  createComment(form: NgForm) {
    //TODO check account, or login

    if (!form.value.comment || form.value.comment.length < 1)
      return;

    const credentials = {
      'placeID': this.placeinfo.id_place,
      'commentText': form.value.comment
    }
    
    this.errorMessage = "";

    console.log(credentials);
    
    this.http.post(this.baseUrl + "place/createcomment", credentials)
        .subscribe(response => {
          this.http.get<CommentsComponent[]>(this.baseUrl + 'place/placeinfocomments?id=' +
              this.placeinfo.id_place).subscribe(result => {
            this.comment_list = result;
          }, error => console.error(error));

        }, error => {
          this.errorMessage = error.error;
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
}
