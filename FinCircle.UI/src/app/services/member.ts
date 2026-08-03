import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../enviroments/enviroment';

export interface Member {
  id: number;
  memberCode: string;
  fullName: string;
  phoneNumber: string;
  email: string;
  address: string;
  joinedOn: string;
  isActive: boolean;
}

export interface CreateMemberDto {
  fullName: string;
  phoneNumber: string;
  email: string;
  address: string;
}

export interface UpdateMemberDto {
  fullName: string;
  phoneNumber: string;
  email: string;
  address: string;
}

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl + '/member';

  private getAuthHeaders() {
    const token = localStorage.getItem('token') || '';
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      }),
    };
  }

  getAllMembers(): Observable<Member[]> {
    return this.http.get<Member[]>(this.apiUrl, this.getAuthHeaders());
  }

  getMemberById(id: number): Observable<Member> {
    return this.http.get<Member>(`${this.apiUrl}/${id}`, this.getAuthHeaders());
  }

  createMember(dto: CreateMemberDto): Observable<any> {
    return this.http.post(this.apiUrl, dto, this.getAuthHeaders());
  }

  updateMember(id: number, dto: UpdateMemberDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, dto, this.getAuthHeaders());
  }

  deleteMember(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`, this.getAuthHeaders());
  }
}

