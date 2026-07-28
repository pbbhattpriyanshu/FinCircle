import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Navbar } from '../../components/navbar/navbar';
import { ContributionService, Contribution as ContributionModel, CreateContributionDto } from '../../services/contribution';
import { MemberService, Member as MemberModel } from '../../services/member';

@Component({
  selector: 'app-contribution',
  standalone: true,
  imports: [CommonModule, FormsModule, Navbar],
  templateUrl: './contribution.html',
  styleUrl: './contribution.css',
})
export class Contribution implements OnInit {
  private contributionService = inject(ContributionService);
  private memberService = inject(MemberService);

  contributions: ContributionModel[] = [];
  filteredContributions: ContributionModel[] = [];
  members: MemberModel[] = [];

  searchTerm: string = '';
  selectedYear: string = 'All';
  availableYears: number[] = [2026, 2025, 2024];

  monthsList = [
    { id: 1, name: 'January' },
    { id: 2, name: 'February' },
    { id: 3, name: 'March' },
    { id: 4, name: 'April' },
    { id: 5, name: 'May' },
    { id: 6, name: 'June' },
    { id: 7, name: 'July' },
    { id: 8, name: 'August' },
    { id: 9, name: 'September' },
    { id: 10, name: 'October' },
    { id: 11, name: 'November' },
    { id: 12, name: 'December' },
  ];

  paymentMethods = ['Bank Transfer', 'Cash', 'UPI', 'Credit Card', 'Cheque'];

  isLoading: boolean = true;
  isSaving: boolean = false;
  errorMessage: string = '';
  successMessage: string = '';

  showModal: boolean = false;

  formData: CreateContributionDto = {
    memberId: 0,
    amount: 500,
    contributionMonth: new Date().getMonth() + 1,
    contributionYear: new Date().getFullYear(),
    paymentDate: new Date().toISOString().split('T')[0],
    paymentMethod: 'Bank Transfer',
    remarks: '',
  };

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.memberService.getAllMembers().subscribe({
      next: (memRes) => {
        this.members = memRes || [];
        if (this.members.length > 0 && !this.formData.memberId) {
          this.formData.memberId = this.members[0].id;
        }

        this.contributionService.getAllContributions().subscribe({
          next: (contribRes) => {
            this.contributions = contribRes || [];
            this.filterContributions();
            this.isLoading = false;
          },
          error: (err) => {
            this.isLoading = false;
            this.errorMessage = err?.error?.message || 'Failed to load contributions list from backend.';
          },
        });
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = 'Failed to load circle members directory.';
      },
    });
  }

  filterContributions(): void {
    let result = [...this.contributions];

    if (this.selectedYear !== 'All') {
      const year = parseInt(this.selectedYear, 10);
      result = result.filter((c) => c.contributionYear === year);
    }

    if (this.searchTerm.trim()) {
      const term = this.searchTerm.toLowerCase().trim();
      result = result.filter((c) => {
        const memberName = this.getMemberName(c.memberId).toLowerCase();
        const monthName = this.getMonthName(c.contributionMonth).toLowerCase();
        return (
          memberName.includes(term) ||
          c.paymentMethod.toLowerCase().includes(term) ||
          c.remarks.toLowerCase().includes(term) ||
          monthName.includes(term) ||
          c.amount.toString().includes(term)
        );
      });
    }

    this.filteredContributions = result;
  }

  setYearFilter(year: string): void {
    this.selectedYear = year;
    this.filterContributions();
  }

  getMemberName(memberId: number): string {
    const member = this.members.find((m) => m.id === memberId);
    return member ? member.fullName : `Member #${memberId}`;
  }

  getMemberCode(memberId: number): string {
    const member = this.members.find((m) => m.id === memberId);
    return member ? member.memberCode : `FC#${memberId}`;
  }

  getMonthName(monthNum: number): string {
    const m = this.monthsList.find((item) => item.id === monthNum);
    return m ? m.name : `Month ${monthNum}`;
  }

  openCreateModal(): void {
    if (this.members.length === 0) {
      this.errorMessage = 'Please register at least one member before recording a contribution.';
      return;
    }

    const today = new Date();
    this.formData = {
      memberId: this.members[0].id,
      amount: 500,
      contributionMonth: today.getMonth() + 1,
      contributionYear: today.getFullYear(),
      paymentDate: today.toISOString().split('T')[0],
      paymentMethod: 'Bank Transfer',
      remarks: '',
    };

    this.errorMessage = '';
    this.successMessage = '';
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
  }

  saveContribution(): void {
    if (!this.formData.memberId || !this.formData.amount || !this.formData.contributionMonth || !this.formData.contributionYear) {
      this.errorMessage = 'Please fill out all required contribution fields.';
      return;
    }

    this.isSaving = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.contributionService.createContribution(this.formData).subscribe({
      next: (res) => {
        this.isSaving = false;
        this.successMessage = res?.message || 'Contribution deposit recorded successfully!';
        this.closeModal();
        this.loadData();
      },
      error: (err) => {
        this.isSaving = false;
        this.errorMessage = err?.error?.message || 'Failed to record contribution deposit.';
      },
    });
  }

  get totalAccumulatedVolume(): number {
    return this.contributions.reduce((sum, c) => sum + (c.amount || 0), 0);
  }

  get totalDepositsCount(): number {
    return this.contributions.length;
  }

  get avgDepositAmount(): number {
    if (this.contributions.length === 0) return 0;
    return this.totalAccumulatedVolume / this.contributions.length;
  }

  get activeContributorsCount(): number {
    const uniqueMembers = new Set(this.contributions.map((c) => c.memberId));
    return uniqueMembers.size;
  }
}

