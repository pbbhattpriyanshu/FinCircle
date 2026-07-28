import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Dashboard } from './pages/dashboard/dashboard';
import { Member } from './pages/member/member';
import { Loan } from './pages/loan/loan';
import { Contribution } from './pages/contribution/contribution';

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
    path: 'dashboard',
    component: Dashboard
  },
  {
    path: 'members',
    component: Member
  },
  {
    path: 'loans',
    component: Loan
  },
  {
    path: 'contributions',
    component: Contribution
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];