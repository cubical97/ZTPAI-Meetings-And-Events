import { Component, Inject} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {CommentsComponent} from "./comments/comments.component";
import {NgForm} from "@angular/forms";
import {PLaceInfoDataMeeting} from "./place-meeting-info/place-meeting-info.component";
import {PLaceInfoDataPlace} from "./place-place-info/place-place-info.component";
import {CookieService} from "ngx-cookie-service";

@Component({
  selector: 'app-place-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class PlaceInfoComponent {
  placeinfo: PlaceInfo;
  comment_list: CommentsComponent[];
  errorMessage: string;
  multitime: boolean;

  private baseUrl: string;
  private http: HttpClient;

  private dateinfometting: PLaceInfoDataMeeting;
  private dateinfoplace: PLaceInfoDataPlace;

  private is_logged: boolean;

  private is_like: boolean;
  private is_dislike: boolean;

  private is_join: boolean;
  private is_follow: boolean;

  private colors: string[];
  private color_like: number;
  private color_dislike: number;

  constructor(private route: ActivatedRoute, @Inject('BASE_URL') baseUrl: string, http: HttpClient,
              private cookieService: CookieService) {
    this.is_like = false;
    this.is_dislike = true;

    this.colors = ["black", "green", "red"];
    this.color_like = 0;
    this.color_dislike = 0;

    this.is_join = false;
    this.is_follow = true;

    this.is_logged = false;

    this.baseUrl = baseUrl;
    this.http = http;
    this.multitime = false;

    var id = this.route.snapshot.paramMap.get('id');
    this.http.get<PlaceInfo>(this.baseUrl + 'place/placeinfo?id=' + id).subscribe(result => {
      this.placeinfo = result;
      this.multitime = this.placeinfo.multitime;
      if (this.multitime) {
        this.http.get<PLaceInfoDataPlace>(this.baseUrl + 'place/placeinfodataplace?id=' + id).subscribe(result => {
          this.dateinfoplace = result;
        }, error => console.error(error));
        this.dateinfometting = null;
      } else {
        this.http.get<PLaceInfoDataMeeting>(this.baseUrl + 'place/placeinfodatameeting?id=' + id).subscribe(result => {
          this.dateinfometting = result;
        }, error => console.error(error));
        this.dateinfoplace = null;
      }
    }, error => console.error(error));

    this.http.get<CommentsComponent[]>(this.baseUrl + 'place/placeinfocomments?id=' + id).subscribe(result => {
      this.comment_list = result;
    }, error => console.error(error));
  }
  
  ngOnInit() {
    if (this.cookieService.check('meetings-and-events-logged')) {
      if (this.cookieService.get('meetings-and-events-logged') === "true") {
        this.is_logged = true;
        this.check_likes();
        this.check_follows();
      } else {
        this.is_logged = false;
      }
    }
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

  check_likes() {
    //TODO
  }

  check_follows() {
    //TODO
  }

  like_switch() {
    console.log("TODO like");
    //TODO
  }

  dislike_switch() {
    console.log("TODO dislike");
    //TODO
  }

  join_switch() {
    console.log("TODO join");
    //TODO
  }

  follow_switch() {
    console.log("TODO follow");
    //TODO
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
