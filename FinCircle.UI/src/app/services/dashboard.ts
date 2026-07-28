import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface DashboardData {
  totalMembers: number;
  activeMembers: number;
  totalLoans: number;
  approvedLoans: number;
  pendingLoans: number;
  totalContributions: number;
  totalContributionAmount: number;
}

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7070/api/dashboard';

  getDashboardData(): Observable<DashboardData> {
    return this.http.get<DashboardData>(this.apiUrl);
  }
}

