import {Component, Inject, Input} from '@angular/core';
import {NgForm} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {PLaceInfoDataMeeting} from "../../info/place-meeting-info/place-meeting-info.component";

@Component({
  selector: 'app-edit-place-meeting',
  templateUrl: './edit-place-meeting.component.html',
  styleUrls: ['./edit-place-meeting.component.css']
})
export class EditPlaceMeetingComponent {
  @Input() placeId: number;

  errorMessage: string;

  private http: HttpClient;
  private baseUrl: string;

  private dateinfometting: PLaceInfoDataMeeting;

  private old_date: string;
  private old_starttime: string;
  private old_endtime: string;

  private new_date: string;
  private new_starttime: string;
  private new_endtime: string;

  constructor(private router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.http.get<PLaceInfoDataMeeting>(this.baseUrl + 'place/placeinfodatameeting?id=' + this.placeId).subscribe(result => {
      this.dateinfometting = result;
      this.new_date = this.old_date = this.dateinfometting.date;
      this.new_starttime = this.old_starttime = this.dateinfometting.starttime;
      this.new_endtime = this.old_endtime = this.dateinfometting.endtime;
    }, error => console.error(error));
  }

  onKeyStarttime(event: any) {
    this.new_starttime = event.target.value;
  }

  onKeyEndtime(event: any) {
    this.new_endtime = event.target.value;
  }

  changeDate(form: NgForm) {
    this.errorMessage = "";
    if (form.value.dp)
      this.new_date = form.value.dp;

    if (this.new_starttime >= this.new_endtime) {
      this.errorMessage = "End time must be after start time\n";
      return;
    }
    const credentials = {
      'datepicker': this.new_date,
      'timeOC:': [this.new_starttime, this.new_endtime]
    }
    console.log(credentials);
  }
}
