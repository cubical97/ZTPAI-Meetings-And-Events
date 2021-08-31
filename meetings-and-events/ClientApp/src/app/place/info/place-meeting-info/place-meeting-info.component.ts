import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-place-meeting-info',
  templateUrl: './place-meeting-info.component.html',
  styleUrls: ['./place-meeting-info.component.css']
})

export class PlaceMeetingInfoComponent {
  @Input() date: string;
  @Input() starttime: string;
  @Input() endtime: string;
}

export class PLaceInfoDataMeeting {
  date: string;
  starttime: string
  endtime: string;
}
