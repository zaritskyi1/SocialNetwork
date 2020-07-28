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
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'friends',
        component: FriendshipInfoComponent,
        resolve: {friendships: FriendListResolver},
        canActivate: [AuthGuard],
        runGuardsAndResolvers: 'always'
    },
    {
        path: 'user/edit',
        component: UserEditComponent,
        resolve: { user: UserEditResolver},
        canActivate: [AuthGuard],
        runGuardsAndResolvers: 'always'
    },
    {
        path: 'user/:id',
        component: UserDetailComponent,
        resolve: { user: UserDetailResolver },
        canActivate: [AuthGuard],
        runGuardsAndResolvers: 'always'
    },
    {
        path: 'users',
        component: UserListComponent,
        resolve: { users: UserListResolver },
        canActivate: [AuthGuard],
        runGuardsAndResolvers: 'always'
    },
    {
        path: 'conversations/:id',
        component: ConversationDetailComponent,
        resolve: { conversation: ConversationDetailResolver },
        canActivate: [AuthGuard],
        runGuardsAndResolvers: 'always'
    },
    {
        path: 'conversations',
        component: ConversationListComponent,
        resolve: { conversations: ConversationListResolver },
        canActivate: [AuthGuard],
        runGuardsAndResolvers: 'always'
    },
    {
        path: 'admin',
        component: AdminPanelComponent,
        data: {roles: ['Administrator', 'Moderator']},
        canActivate: [AuthGuard],
        runGuardsAndResolvers: 'always'
    },
    {
        path: '**',
        redirectTo: 'home',
        pathMatch: 'full'
    }
];
