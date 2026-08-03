import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Dashboard } from './pages/dashboard/dashboard';
import { Member } from './pages/member/member';
import { Loan } from './pages/loan/loan';
import { Contribution } from './pages/contribution/contribution';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'login'
  },
  {
    path: 'login',
    component: Login
  },
  {
    path: 'signup',
    component: Register
  },
  {
    path: 'dashboard',
    component: Dashboard,
    canActivate: [authGuard]
  },
  {
    path: 'members',
    component: Member,
    canActivate: [authGuard]
  },
  {
    path: 'loans',
    component: Loan,
    canActivate: [authGuard]
  },
  {
    path: 'contributions',
    component: Contribution,
    canActivate: [authGuard]
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];