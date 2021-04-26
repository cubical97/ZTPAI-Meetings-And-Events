import { Component } from '@angular/core';

@Component({
  selector: 'app-tile',
  templateUrl: './tile.component.html',
  styleUrls: ['./tile.component.css']
})
export class PlaceTileComponent {
  public tile_id: number = 1;
  public title: string = "Tytuł";
  public image: string = "testimage.jpg";
  public description: string = "Przykładowy opis, przykładowy opis, przykładowy opis, przykładowy opis.";
  public address: string = "Kraków 24 Wiśniowa";
  public likes: number = 42;
  public dislikes: number = 1;
}
