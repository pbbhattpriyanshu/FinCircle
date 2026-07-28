import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Navbar } from '../../components/navbar/navbar';
import { LoanService, Loan as LoanModel, CreateLoanDto } from '../../services/loan';
import { MemberService, Member as MemberModel } from '../../services/member';

@Component({
  selector: 'app-loan',
  standalone: true,
  imports: [CommonModule, FormsModule, Navbar],
  templateUrl: './loan.html',
  styleUrl: './loan.css',
})

export class Loan implements OnInit {
  private loanService = inject(LoanService);
  private memberService = inject(MemberService);

  loans: LoanModel[] = [];
  filteredLoans: LoanModel[] = [];
  members: MemberModel[] = [];

  selectedStatus: string = 'All';
  searchTerm: string = '';

  isLoading: boolean = true;
  isSaving: boolean = false;
  processingLoanId: number | null = null;
  errorMessage: string = '';
  successMessage: string = '';

  showModal: boolean = false;

  formData: CreateLoanDto = {
    memberId: 0,
    loanAmount: 5000,
    interestRate: 7.5,
    durationMonths: 12,
    purpose: '',
  };

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.isLoading = true;
    this.errorMessage = '';

    // Fetch members and loans
    this.memberService.getAllMembers().subscribe({
      next: (memRes) => {
        this.members = memRes || [];
        if (this.members.length > 0 && !this.formData.memberId) {
          this.formData.memberId = this.members[0].id;
        }

        this.loanService.getAllLoans().subscribe({
          next: (loanRes) => {
            this.loans = loanRes || [];
            this.filterLoans();
            this.isLoading = false;
          },
          error: (err) => {
            this.isLoading = false;
            this.errorMessage = err?.error?.message || 'Failed to load loans list.';
          },
        });
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = 'Failed to load circle members directory.';
      },
    });
  }

  filterLoans(): void {
    let result = [...this.loans];

    if (this.selectedStatus !== 'All') {
      result = result.filter((l) => l.status.toLowerCase() === this.selectedStatus.toLowerCase());
    }

    if (this.searchTerm.trim()) {
      const term = this.searchTerm.toLowerCase().trim();
      result = result.filter((l) => {
        const memberName = this.getMemberName(l.memberId).toLowerCase();
        return (
          memberName.includes(term) ||
          l.purpose.toLowerCase().includes(term) ||
          l.id.toString().includes(term) ||
          l.loanAmount.toString().includes(term)
        );
      });
    }

    this.filteredLoans = result;
  }

  setStatusFilter(status: string): void {
    this.selectedStatus = status;
    this.filterLoans();
  }

  getMemberName(memberId: number): string {
    const member = this.members.find((m) => m.id === memberId);
    return member ? member.fullName : `Member #${memberId}`;
  }

  getMemberCode(memberId: number): string {
    const member = this.members.find((m) => m.id === memberId);
    return member ? member.memberCode : `FC#${memberId}`;
  }

  calculateMonthlyPayment(amount: number, rate: number, months: number): number {
    if (!amount || !months || months <= 0) return 0;
    const monthlyRate = rate / 100 / 12;
    if (monthlyRate === 0) return amount / months;
    const payment = (amount * monthlyRate * Math.pow(1 + monthlyRate, months)) / (Math.pow(1 + monthlyRate, months) - 1);
    return payment;
  }

  openCreateModal(): void {
    if (this.members.length === 0) {
      this.errorMessage = 'Please register at least one member before applying for a loan.';
      return;
    }

    this.formData = {
      memberId: this.members[0].id,
      loanAmount: 5000,
      interestRate: 7.5,
      durationMonths: 12,
      purpose: '',
    };

    this.errorMessage = '';
    this.successMessage = '';
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
  }

  saveLoan(): void {
    if (!this.formData.memberId || !this.formData.loanAmount || !this.formData.durationMonths || !this.formData.purpose) {
      this.errorMessage = 'Please fill out all required loan fields.';
      return;
    }

    this.isSaving = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.loanService.createLoan(this.formData).subscribe({
      next: (res) => {
        this.isSaving = false;
        this.successMessage = res?.message || 'Loan request submitted successfully!';
        this.closeModal();
        this.loadData();
      },
      error: (err) => {
        this.isSaving = false;
        this.errorMessage = err?.error?.message || 'Failed to submit loan request.';
      },
    });
  }

  approveLoan(loanId: number): void {
    this.processingLoanId = loanId;
    this.errorMessage = '';
    this.successMessage = '';

    this.loanService.approveLoan(loanId).subscribe({
      next: (res) => {
        this.processingLoanId = null;
        this.successMessage = res?.message || 'Loan approved successfully!';
        this.loadData();
      },
      error: (err) => {
        this.processingLoanId = null;
        this.errorMessage = err?.error?.message || 'Failed to approve loan.';
      },
    });
  }

  rejectLoan(loanId: number): void {
    this.processingLoanId = loanId;
    this.errorMessage = '';
    this.successMessage = '';

    this.loanService.rejectLoan(loanId).subscribe({
      next: (res) => {
        this.processingLoanId = null;
        this.successMessage = res?.message || 'Loan rejected.';
        this.loadData();
      },
      error: (err) => {
        this.processingLoanId = null;
        this.errorMessage = err?.error?.message || 'Failed to reject loan.';
      },
    });
  }

  get totalLoanVolume(): number {
    return this.loans.reduce((sum, l) => sum + (l.loanAmount || 0), 0);
  }

  get approvedLoanVolume(): number {
    return this.loans.filter((l) => l.status === 'Approved').reduce((sum, l) => sum + (l.loanAmount || 0), 0);
  }

  get pendingCount(): number {
    return this.loans.filter((l) => l.status === 'Pending').length;
  }

  get approvedCount(): number {
    return this.loans.filter((l) => l.status === 'Approved').length;
  }
}

