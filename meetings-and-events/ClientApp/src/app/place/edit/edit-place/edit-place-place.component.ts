import {Component, Inject, Input} from '@angular/core';
import {NgForm} from "@angular/forms";
import {Router} from "@angular/router";
import {CookieService} from "ngx-cookie-service";
import {HttpClient} from "@angular/common/http";
import {PLaceInfoDataMeeting} from "../../info/place-meeting-info/place-meeting-info.component";
import {PLaceInfoDataPlace} from "../../info/place-place-info/place-place-info.component";

@Component({
  selector: 'app-edit-place-place',
  templateUrl: './edit-place-place.component.html',
  styleUrls: ['./edit-place-place.component.css']
})
export class EditPlacePlaceComponent {
  @Input() placeId: number;

  good_request: boolean;
  errorMessage: string;

  private http: HttpClient;
  private baseUrl: string;

  private dateinfoplace: PLaceInfoDataPlace;

  private old_timeOC1: string[];
  private old_timeOC2: string[];
  private old_timeOC3: string[];
  private old_timeOC4: string[];
  private old_timeOC5: string[];
  private old_timeOC6: string[];
  private old_timeOC7: string[];

  private old_check1: boolean;
  private old_check2: boolean;
  private old_check3: boolean;
  private old_check4: boolean;
  private old_check5: boolean;
  private old_check6: boolean;
  private old_check7: boolean;

  private new_timeOC1: string[];
  private new_timeOC2: string[];
  private new_timeOC3: string[];
  private new_timeOC4: string[];
  private new_timeOC5: string[];
  private new_timeOC6: string[];
  private new_timeOC7: string[];

  private new_check1: boolean;
  private new_check2: boolean;
  private new_check3: boolean;
  private new_check4: boolean;
  private new_check5: boolean;
  private new_check6: boolean;
  private new_check7: boolean;

  constructor(private router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.new_check1 = this.old_check1 = false;
    this.new_check2 = this.old_check2 = false;
    this.new_check3 = this.old_check3 = false;
    this.new_check4 = this.old_check4 = false;
    this.new_check5 = this.old_check5 = false;
    this.new_check6 = this.old_check6 = false;
    this.new_check7 = this.old_check7 = false;
    this.new_timeOC1 = this.old_timeOC1 = ["00:00", "00:00"];
    this.new_timeOC2 = this.old_timeOC2 = ["00:00", "00:00"];
    this.new_timeOC3 = this.old_timeOC3 = ["00:00", "00:00"];
    this.new_timeOC4 = this.old_timeOC4 = ["00:00", "00:00"];
    this.new_timeOC5 = this.old_timeOC5 = ["00:00", "00:00"];
    this.new_timeOC6 = this.old_timeOC6 = ["00:00", "00:00"];
    this.new_timeOC7 = this.old_timeOC7 = ["00:00", "00:00"];
    this.errorMessage = "";
  }

  ngOnInit() {
    this.http.get<PLaceInfoDataPlace>(this.baseUrl + 'place/placeinfodataplace?id=' + this.placeId).subscribe(result => {
      this.dateinfoplace = result;
      if (!this.dateinfoplace.mo.includes("close")) {
        this.new_timeOC1 = this.old_timeOC1 = [this.dateinfoplace.mo.slice(0, 5), this.dateinfoplace.mo.slice(8, 13)];
        this.new_check1 = this.old_check1 = true;
      }
      if (!this.dateinfoplace.tu.includes("close")) {
        this.new_timeOC2 = this.old_timeOC2 = [this.dateinfoplace.tu.slice(0, 5), this.dateinfoplace.tu.slice(8, 13)];
        this.new_check2 = this.old_check2 = true;
      }
      if (!this.dateinfoplace.we.includes("close")) {
        this.new_timeOC3 = this.old_timeOC3 = [this.dateinfoplace.we.slice(0, 5), this.dateinfoplace.we.slice(8, 13)];
        this.new_check3 = this.old_check3 = true;
      }
      if (!this.dateinfoplace.th.includes("close")) {
        this.new_timeOC4 = this.old_timeOC4 = [this.dateinfoplace.th.slice(0, 5), this.dateinfoplace.th.slice(8, 13)];
        this.new_check4 = this.old_check4 = true;
      }
      if (!this.dateinfoplace.fr.includes("close")) {
        this.new_timeOC5 = this.old_timeOC5 = [this.dateinfoplace.fr.slice(0, 5), this.dateinfoplace.fr.slice(8, 13)];
        this.new_check5 = this.old_check5 = true;
      }
      if (!this.dateinfoplace.sat.includes("close")) {
        this.new_timeOC6 = this.old_timeOC6 = [this.dateinfoplace.sat.slice(0, 5), this.dateinfoplace.sat.slice(8, 13)];
        this.new_check6 = this.old_check6 = true;
      }
      if (!this.dateinfoplace.sun.includes("close")) {
        this.new_timeOC7 = this.old_timeOC7 = [this.dateinfoplace.sun.slice(0, 5), this.dateinfoplace.sun.slice(8, 13)];
        this.new_check7 = this.old_check7 = true;
      }
    }, error => console.error(error));
  }

  apply(form: NgForm) {
    this.errorMessage = "";
    var timeday1;
    var timeday2;
    var timeday3;
    var timeday4;
    var timeday5;
    var timeday6;
    var timeday7;

    this.good_request = true;

    timeday1 = this.checkTime(this.new_check1, this.new_timeOC1[0], this.new_timeOC1[1]);
    timeday2 = this.checkTime(this.new_check2, this.new_timeOC2[0], this.new_timeOC2[1]);
    timeday3 = this.checkTime(this.new_check3, this.new_timeOC3[0], this.new_timeOC3[1]);
    timeday4 = this.checkTime(this.new_check4, this.new_timeOC4[0], this.new_timeOC4[1]);
    timeday5 = this.checkTime(this.new_check5, this.new_timeOC5[0], this.new_timeOC5[1]);
    timeday6 = this.checkTime(this.new_check6, this.new_timeOC6[0], this.new_timeOC6[1]);
    timeday7 = this.checkTime(this.new_check7, this.new_timeOC7[0], this.new_timeOC7[1]);

    if (!(timeday1 || timeday2 || timeday3 || timeday4 || timeday5 || timeday6 || timeday7)) {
      this.errorMessage += "No time set\n";
      this.good_request = false;
    }

    if (!this.good_request)
      return;

    const credentials = {
      'place_id': Number(this.placeId),
      'timeOC1': timeday1,
      'timeOC2': timeday2,
      'timeOC3': timeday3,
      'timeOC4': timeday4,
      'timeOC5': timeday5,
      'timeOC6': timeday6,
      'timeOC7': timeday7
    }

    this.http.post(this.baseUrl + "placeedit/editdateplace", credentials)
        .subscribe(response => {
        }, error => {
          console.error(error.error);
        })
  }

  checkTime(checkbox, time1, time2) {
    if (checkbox) {
      if (time1 && time2) {
        if (time1 < time2)
          return [time1, time2];
        else {
          this.errorMessage += "Close time must be after open time\n";
        }
      } else {
        this.errorMessage = "Not every time is set";
        this.good_request = false;
      }
    }
    return null;
  }

  onKeyCheckbox(event: any, id: number) {
    switch (id) {
      case 1:
        this.new_check1 = event.target.checked;
        break;
      case 2:
        this.new_check2 = event.target.checked;
        break;
      case 3:
        this.new_check3 = event.target.checked;
        break;
      case 4:
        this.new_check4 = event.target.checked;
        break;
      case 5:
        this.new_check5 = event.target.checked;
        break;
      case 6:
        this.new_check6 = event.target.checked;
        break;
      case 7:
        this.new_check7 = event.target.checked;
        break;
    }
  }

  onKeyTime(event: any, id: number, close: boolean) {
    switch (id) {
      case 1:
        if (close) this.new_timeOC1[1] = event.target.value;
        else this.new_timeOC1[0] = event.target.value;
        break;
      case 2:
        if (close) this.new_timeOC2[1] = event.target.value;
        else this.new_timeOC2[0] = event.target.value;
        break;
      case 3:
        if (close) this.new_timeOC3[1] = event.target.value;
        else this.new_timeOC3[0] = event.target.value;
        break;
      case 4:
        if (close) this.new_timeOC4[1] = event.target.value;
        else this.new_timeOC4[0] = event.target.value;
        break;
      case 5:
        if (close) this.new_timeOC5[1] = event.target.value;
        else this.new_timeOC5[0] = event.target.value;
        break;
      case 6:
        if (close) this.new_timeOC6[1] = event.target.value;
        else this.new_timeOC6[0] = event.target.value;
        break;
      case 7:
        if (close) this.new_timeOC7[1] = event.target.value;
        else this.new_timeOC7[0] = event.target.value;
        break;
    }
  }

  changeDateClose(form: NgForm) {
    if (!form.value.dp_close)
      return;
    const credentials = {
      'place_id': Number(this.placeId),
      'datepicker': form.value.dp_close
    }

    this.http.post(this.baseUrl + "placeedit/addcloseddate", credentials)
        .subscribe(response => {
        }, error => {
          console.error(error.error);
        })
  }
}
