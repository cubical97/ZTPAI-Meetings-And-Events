import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-place-place-info',
  templateUrl: './place-place-info.component.html',
  styleUrls: ['./place-place-info.component.css']
})

export class PlacePlaceInfoComponent {
  @Input() mo: string
  @Input() tu: string;
  @Input() we: string;
  @Input() th: string;
  @Input() fr: string;
  @Input() sat: string;
  @Input() sun: string;
}

export class PLaceInfoDataPlace {
  mo: string
  tu: string;
  we: string;
  th: string;
  fr: string;
  sat: string;
  sun: string;
}
