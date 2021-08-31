import { Component, Input } from '@angular/core';
import { PlaceTile } from "../tile/tile.component";

@Component({
  selector: 'app-place-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class PlaceListComponent {
  @Input() places: PlaceTile[];
}
