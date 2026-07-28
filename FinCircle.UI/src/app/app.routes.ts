import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
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
    path: 'signup',
    component: Register
  },
  {
    path: 'register',
    component: Register
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