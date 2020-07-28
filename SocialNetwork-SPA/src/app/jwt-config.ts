import { JwtConfig } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

function tokenGetter() {
    return localStorage.getItem('token');
}

export const jwtConfig: JwtConfig = {
    tokenGetter,
    whitelistedDomains: [environment.domain],
    blacklistedRoutes: [`${environment.apiUrl}/auth`, `${environment.apiUrl}/register`],
};
