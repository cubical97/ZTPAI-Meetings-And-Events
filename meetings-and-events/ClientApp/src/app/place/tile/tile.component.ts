import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-tile',
  templateUrl: './tile.component.html',
  styleUrls: ['./tile.component.css']
})
export class PlaceTileComponent {
  @Input() tile_id: number
  @Input() title: string;
  @Input() image: string;
  @Input() description: string;
  @Input() address: string;
  @Input() likes: number;
  @Input() dislikes: number;
  @Input() username: string;
  @Input() multitibe: boolean;
}

export class PlaceTile {
  id_place: number;
  title: string;
  description: string;
  image: string;
  users_username: string;
  address_city: string;
  rate_likes: number;
  rate_dislikes: number;
  multitibe: boolean;

  ngOnInit() {
    if (!this.description || this.description.length < 1)
      this.description = " ";
  }
}
