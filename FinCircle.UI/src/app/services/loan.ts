import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Loan {
  id: number;
  memberId: number;
  loanAmount: number;
  interestRate: number;
  durationMonths: number;
  purpose: string;
  status: string;
  approvedDate?: string | null;
  createdOn: string;
}

export interface CreateLoanDto {
  memberId: number;
  loanAmount: number;
  interestRate: number;
  durationMonths: number;
  purpose: string;
}

@Injectable({
  providedIn: 'root',
})
export class LoanService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7070/api/loan';

  private getAuthHeaders() {
    const token = localStorage.getItem('token') || '';
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      }),
    };
  }

  getAllLoans(): Observable<Loan[]> {
    return this.http.get<Loan[]>(this.apiUrl, this.getAuthHeaders());
  }

  getLoanById(id: number): Observable<Loan> {
    return this.http.get<Loan>(`${this.apiUrl}/${id}`, this.getAuthHeaders());
  }

  createLoan(dto: CreateLoanDto): Observable<any> {
    return this.http.post(this.apiUrl, dto, this.getAuthHeaders());
  }

  approveLoan(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/approve`, {}, this.getAuthHeaders());
  }

  rejectLoan(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/reject`, {}, this.getAuthHeaders());
  }
}

