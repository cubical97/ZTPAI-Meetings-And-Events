import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})

export class CommentsComponent {
  @Input() comment_id: number
  @Input() author: string;
  @Input() createdate: string;
  @Input() comment: string;
}

export class CommentTile {
  comment_id: number
  author: string;
  createdate: string;
  comment: string;
}
