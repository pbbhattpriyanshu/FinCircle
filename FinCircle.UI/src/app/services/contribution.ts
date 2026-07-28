import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Contribution {
  id: number;
  memberId: number;
  amount: number;
  contributionMonth: number;
  contributionYear: number;
  paymentDate: string;
  paymentMethod: string;
  status: string;
  remarks: string;
  createdOn: string;
}

export interface CreateContributionDto {
  memberId: number;
  amount: number;
  contributionMonth: number;
  contributionYear: number;
  paymentDate: string;
  paymentMethod: string;
  remarks: string;
}

@Injectable({
  providedIn: 'root',
})
export class ContributionService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7070/api/contribution';

  private getAuthHeaders() {
    const token = localStorage.getItem('token') || '';
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      }),
    };
  }

  getAllContributions(): Observable<Contribution[]> {
    return this.http.get<Contribution[]>(this.apiUrl, this.getAuthHeaders());
  }

  getContributionById(id: number): Observable<Contribution> {
    return this.http.get<Contribution>(`${this.apiUrl}/${id}`, this.getAuthHeaders());
  }

  createContribution(dto: CreateContributionDto): Observable<any> {
    return this.http.post(this.apiUrl, dto, this.getAuthHeaders());
  }
}

