import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu-user',
  templateUrl: './nav-menu-user.component.html',
  styleUrls: ['./nav-menu-user.component.css']
})
export class NavMenuUserComponent {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
