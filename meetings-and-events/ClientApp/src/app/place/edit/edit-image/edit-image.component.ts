import {Component, Inject, Input} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-edit-image',
  templateUrl: './edit-image.component.html',
  styleUrls: ['./edit-image.component.css']
})
export class EditImageComponent {
  @Input() placeIdIn: number;
  @Input() oldImageIn: string;

  private placeId: number;
  private oldImage: string;
  private newImage: string;

  private isImage: boolean;
  private isNewImage: boolean;

  errorMessage: string;

  private http: HttpClient;
  private baseUrl: string;

  constructor(private router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.isNewImage = false;
  }

  ngOnInit() {
    this.placeId = this.placeIdIn;
    this.oldImage = this.oldImageIn;
    if (this.oldImage)
      this.isImage = true;
  }
}
