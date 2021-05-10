import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { AppComponent } from './app.component';

import { NavMenuComponent } from './nav/nav-menu/nav-menu.component';

import { SignInComponent } from './logging/signin/signin.component';
import { SignUpComponent} from './logging/signup/signup.component';
import { LogOutComponent} from './logging/logout/logout.component';

import { HomeComponent } from './home/home.component';

import { PlaceTileComponent} from './place/tile/tile.component';
import { PlaceInfoComponent} from './place/info/info.component';
import { PlacePlaceInfoComponent} from './place/info/place-place-info/place-place-info.component';
import { PlaceMeetingInfoComponent} from './place/info/place-meeting-info/place-meeting-info.component';
import { CommentsComponent} from './place/info/comments/comments.component';

import { CreatePlaceComponent} from './place/create/create-place.component';
import { CreatePlacePlaceComponent} from './place/create/create-place/create-place-place.component';
import { CreatePlaceMeetingComponent} from './place/create/create-meeting/create-place-meeting.component';

import { UserInfoComponent} from './user/user-info/user-info.component';
import { UserCreatedPlacesComponent} from './user/user-created-places/user-created-places.component';
import { UserListFollowComponent} from './user/user-list-follow/user-list-follow.component';
import { UserListJoinComponent} from './user/user-list-join/user-list-join.component';
import { UserOptionsComponent} from './user/user-options/user-options.component';

import { CookieService } from "ngx-cookie-service";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SignInComponent,
    SignUpComponent,
    LogOutComponent,
    HomeComponent,
    PlaceTileComponent,
    PlaceInfoComponent,
    PlacePlaceInfoComponent,
    PlaceMeetingInfoComponent,
    CommentsComponent,
    CreatePlaceComponent,
    CreatePlacePlaceComponent,
    CreatePlaceMeetingComponent,
    UserInfoComponent,
    UserCreatedPlacesComponent,
    UserListFollowComponent,
    UserListJoinComponent,
    UserOptionsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'signin', component: SignInComponent },
      { path: 'signup', component: SignUpComponent },

      { path: 'logout', component: LogOutComponent },
        
      { path: 'addplace', component: CreatePlaceComponent },
      { path: 'joined', component: UserListJoinComponent },
      { path: 'follows', component: UserListFollowComponent },
      { path: 'mypalces', component: UserCreatedPlacesComponent },
      { path: 'options', component: UserOptionsComponent }
    ])
  ],
  providers: [ CookieService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
