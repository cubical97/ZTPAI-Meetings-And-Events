import {Component, Inject, Input} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {timer} from "rxjs";

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

  private selectedFile: File;

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

  onFileChanged(event) {
    this.selectedFile = event.target.files[0];
  }

  onUpload() {
    this.isNewImage = false;
    if (!this.selectedFile)
      return;

    const uploadData = new FormData();
    uploadData.append('place_id', this.placeId.toString());
    uploadData.append('myImage', this.selectedFile, this.selectedFile.name);

    this.http.post(this.baseUrl + "placeedit/editimage", uploadData, {
      reportProgress: true,
      observe: 'events'
    })
        .subscribe(event => {
        }, error => {
          console.error(error.error);
        }, () => {
          this.newImage = "Uploads\\" + this.placeId + '.' + this.selectedFile.type.split('/')[1];
          setTimeout(() => {
            this.isNewImage = true;
          }, 1000);
        })
  }
}
