import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserDetailComponent } from './user/user-detail/user-detail.component';
import { UserDetailResolver } from './_resolvers/user-detail.resolver';
import { FriendListResolver } from './_resolvers/friend-list.resolver';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { ConversationListResolver } from './_resolvers/conversation-list.resolver';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { ConversationListComponent } from './conversation/conversation-list/conversation-list.component';
import { ConversationDetailComponent } from './conversation/conversation-detail/conversation-detail.component';
import { FriendshipInfoComponent } from './friend/friendship-info/friendship-info.component';
import { ConversationDetailResolver } from './_resolvers/conversaton-detail.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { UserListComponent } from './user/user-list/user-list.component';

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
            { path: 'user/edit', component: UserEditComponent, resolve: { user: UserEditResolver} },
            { path: 'user/:id', component: UserDetailComponent, resolve: { user: UserDetailResolver } },
            { path: 'users', component: UserListComponent, resolve: { users: UserListResolver } },
            { path: 'conversations/:id', component: ConversationDetailComponent, resolve: { conversation: ConversationDetailResolver } },
            { path: 'conversations', component: ConversationListComponent, resolve: { conversations: ConversationListResolver } },
            { path: 'admin', component: AdminPanelComponent, data: {roles: ['Administrator', 'Moderator']} }
        ]
    },
    { path: '**', redirectTo: 'home', pathMatch: 'full' }
];
