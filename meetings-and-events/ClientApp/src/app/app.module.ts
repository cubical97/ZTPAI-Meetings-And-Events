import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AppLoggedComponent } from './app-logged.component';
import { NavMenuComponent } from './nav/nav-menu/nav-menu.component';
import { NavMenuUserComponent } from './nav/nav-menu-user/nav-menu-user.component';
import { HomeComponent } from './home/home.component';

import { SignInComponent } from './logging/signin/signin.component';
import { SignUpComponent} from './logging/signup/signup.component';
import { LogOutComponent} from './logging/logout/logout.component';

import { EventTileComponent} from './events/event-tile/event-tile.component';
import { CreateEventComponent} from './events/create/create-event.component';
import { EventInfoComponent} from './events/event-info/event-info.component';

@NgModule({
  declarations: [
    AppComponent,
    AppLoggedComponent,
    NavMenuComponent,
    NavMenuUserComponent,
    HomeComponent,
    SignInComponent,
    SignUpComponent,
    LogOutComponent,
    EventTileComponent,
    CreateEventComponent,
    EventInfoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'signin', component: SignInComponent },
      { path: 'signup', component: SignUpComponent },
      { path: 'logging', component: AppLoggedComponent },

      { path: 'logout', component: LogOutComponent },
      { path: 'create-event', component: CreateEventComponent },
      { path: 'event-info', component: EventInfoComponent },

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
