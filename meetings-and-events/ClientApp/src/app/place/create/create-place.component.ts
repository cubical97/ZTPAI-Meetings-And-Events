import { Component, Inject } from '@angular/core';
import { UploadDownloadService } from "../../../services/upload-download.service";
import {ProgressStatus, ProgressStatusEnum} from "../../../models/progress-status.model";

@Component({
  selector: 'app-create-place',
  templateUrl: './create-place.component.html',
  styleUrls: ['./create-place.component.css']
})
export class CreatePlaceComponent {
  switch_expression: string = "default";
  match_expression_1: string = "one";
  match_expression_2: string = "multi";

  constructor() { }
  
  setModeOne() {
    this.switch_expression = this.match_expression_1;
  }

  setModeMulti() {
    this.switch_expression = this.match_expression_2;
  }
  
}
