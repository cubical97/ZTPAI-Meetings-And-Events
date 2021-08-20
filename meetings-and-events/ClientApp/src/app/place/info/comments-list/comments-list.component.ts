import { Component, Input } from '@angular/core';
import { CommentTile } from "../comments/comments.component";

@Component({
  selector: 'app-comment-list',
  templateUrl: './comments-list.component.html',
  styleUrls: ['./comments-list.component.css']
})
export class CommentsListComponent {
  @Input() comments: CommentTile[];
}
