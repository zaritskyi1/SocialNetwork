import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { FriendListComponent } from './friend/friend-list/friend-list.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserDetailComponent } from './user/user-detail/user-detail.component';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';
import { FriendListResolver } from './_resolvers/friend-list.resolver';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changed.guard';
import { ConversationListResolver } from './_resolvers/conversation-list.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { MessageManagementComponent } from './admin/message-management/message-management.component';
import { ConversationListComponent } from './conversation/conversation-list/conversation-list.component';
import { ConversationDetailComponent } from './conversation/conversation-detail/conversation-detail.component';
import { FriendshipInfoComponent } from './friend/friendship-info/friendship-info.component';

export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'friends', component: FriendshipInfoComponent, resolve: {friendships: FriendListResolver} },
            { path: 'user/edit', component: UserEditComponent, resolve: { user: UserEditResolver}, canDeactivate: [PreventUnsavedChanges] },
            { path: 'user/:id', component: UserDetailComponent, resolve: { user: UserDetailResolver } },
            { path: 'conversations/:id', component: ConversationDetailComponent },
            { path: 'conversations', component: ConversationListComponent, resolve: { conversations: ConversationListResolver } },
            { path: 'admin', component: AdminPanelComponent, data: {roles: ['Administrator', 'Moderator']} },
            { path: 'admin/users', component: UserManagementComponent },
            { path: 'admin/messages', component: MessageManagementComponent },
        ]
    },
    { path: '**', redirectTo: 'home', pathMatch: 'full' }
];
