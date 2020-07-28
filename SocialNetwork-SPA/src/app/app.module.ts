import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { AuthService } from './_services/auth.service';
import { ErrorInterceptorProvider } from './_interceptors/error.interceptor';
import { FriendListComponent } from './friend/friend-list/friend-list.component';
import { appRoutes } from './routes';
import { UserDetailComponent } from './user/user-detail/user-detail.component';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';
import { FriendListResolver } from './_resolvers/friend-list.resolver';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { ConversationListResolver } from './_resolvers/conversation-list.resolver';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { MessageManagementComponent } from './admin/message-management/message-management.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { RolesEditDialogComponent } from './admin/roles-edit-dialog/roles-edit-dialog.component';
import { MessageDetailDialogComponent } from './admin/message-detail-dialog/message-detail-dialog.component';
import { UserDetailDialogComponent } from './admin/user-detail-dialog/user-detail-dialog.component';
import { ConversationListComponent } from './conversation/conversation-list/conversation-list.component';
import { MessageListComponent } from './message/message-list/message-list.component';
import { ConversationDetailComponent } from './conversation/conversation-detail/conversation-detail.component';
import { FriendshipInfoComponent } from './friend/friendship-info/friendship-info.component';
import { FriendshipRequestListComponent } from './friend/friendship-request-list/friendship-request-list.component';
import { ConversationDetailResolver } from './_resolvers/conversaton-detail.resolver';
import { UserCardComponent } from './user/user-card/user-card.component';
import { TimeAgoExtendsPipe } from './_pipes/time-ago-extends.pipe';
import { FriendshipActionsComponent } from './friend/friendship-actions/friendship-actions.component';
import { ConversationActionsComponent } from './conversation/conversation-actions/conversation-actions.component';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { UserListComponent } from './user/user-list/user-list.component';
import { MessageFormComponent } from './message/message-form/message-form.component';
import { jwtConfig } from './jwt-config';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      LoginComponent,
      FriendListComponent,
      UserDetailComponent,
      UserEditComponent,
      AdminPanelComponent,
      UserManagementComponent,
      MessageManagementComponent,
      RolesEditDialogComponent,
      MessageDetailDialogComponent,
      UserDetailDialogComponent,
      ConversationListComponent,
      ConversationDetailComponent,
      MessageListComponent,
      FriendshipInfoComponent,
      FriendshipRequestListComponent,
      UserCardComponent,
      FriendshipActionsComponent,
      ConversationActionsComponent,
      UserListComponent,
      MessageFormComponent,
      TimeAgoExtendsPipe
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      FormsModule,
      MaterialModule,
      ReactiveFormsModule,
      HttpClientModule,
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
         config: jwtConfig
      })
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      UserDetailResolver,
      FriendListResolver,
      UserEditResolver,
      ConversationListResolver,
      ConversationDetailResolver,
      UserListResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
