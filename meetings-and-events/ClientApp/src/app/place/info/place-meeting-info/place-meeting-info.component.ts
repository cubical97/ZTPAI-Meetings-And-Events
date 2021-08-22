import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-place-meeting-info',
  templateUrl: './place-meeting-info.component.html',
  styleUrls: ['./place-meeting-info.component.css']
})

export class PlaceMeetingInfoComponent {
  @Input() startdate: string;
  @Input() enddate: string;
}

export class PLaceInfoDataMeeting {
  startdate: string
  enddate: string;
}
