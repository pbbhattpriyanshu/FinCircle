import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Navbar } from '../../components/navbar/navbar';
import { MemberService, Member as MemberModel, CreateMemberDto } from '../../services/member';

@Component({
  selector: 'app-member',
  standalone: true,
  imports: [CommonModule, FormsModule, Navbar],
  templateUrl: './member.html',
  styleUrl: './member.css',
})

export class Member implements OnInit {
  private memberService = inject(MemberService);

  members: MemberModel[] = [];
  filteredMembers: MemberModel[] = [];
  searchTerm: string = '';

  isLoading: boolean = true;
  isSaving: boolean = false;
  isDeleting: boolean = false;
  errorMessage: string = '';
  successMessage: string = '';

  // Modal State
  showModal: boolean = false;
  isEditing: boolean = false;
  editingMemberId: number | null = null;

  // Delete Confirm State
  showDeleteConfirmModal: boolean = false;
  deletingMember: MemberModel | null = null;

  formData: CreateMemberDto = {
    fullName: '',
    phoneNumber: '',
    email: '',
    address: '',
  };

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.memberService.getAllMembers().subscribe({
      next: (res) => {
        this.members = res || [];
        this.filterMembers();
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        if (err?.status === 401) {
          this.errorMessage = 'Unauthorized: Please log in to access the Members directory.';
        } else {
          this.errorMessage = err?.error?.message || 'Failed to load members from backend server.';
        }
      },
    });
  }

  filterMembers(): void {
    if (!this.searchTerm.trim()) {
      this.filteredMembers = [...this.members];
      return;
    }

    const term = this.searchTerm.toLowerCase().trim();
    this.filteredMembers = this.members.filter(
      (m) =>
        m.fullName.toLowerCase().includes(term) ||
        m.memberCode.toLowerCase().includes(term) ||
        m.email.toLowerCase().includes(term) ||
        m.phoneNumber.toLowerCase().includes(term) ||
        m.address.toLowerCase().includes(term)
    );
  }

  openCreateModal(): void {
    this.isEditing = false;
    this.editingMemberId = null;
    this.formData = {
      fullName: '',
      phoneNumber: '',
      email: '',
      address: '',
    };
    this.errorMessage = '';
    this.successMessage = '';
    this.showModal = true;
  }

  openEditModal(member: MemberModel): void {
    this.isEditing = true;
    this.editingMemberId = member.id;
    this.formData = {
      fullName: member.fullName,
      phoneNumber: member.phoneNumber,
      email: member.email,
      address: member.address,
    };
    this.errorMessage = '';
    this.successMessage = '';
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
    this.isEditing = false;
    this.editingMemberId = null;
  }

  saveMember(): void {
    if (!this.formData.fullName || !this.formData.phoneNumber || !this.formData.email || !this.formData.address) {
      this.errorMessage = 'Please fill out all required member fields.';
      return;
    }

    this.isSaving = true;
    this.errorMessage = '';
    this.successMessage = '';

    if (this.isEditing && this.editingMemberId) {
      this.memberService.updateMember(this.editingMemberId, this.formData).subscribe({
        next: (res) => {
          this.isSaving = false;
          this.successMessage = res?.message || 'Member updated successfully!';
          this.closeModal();
          this.loadMembers();
        },
        error: (err) => {
          this.isSaving = false;
          this.errorMessage = err?.error?.message || 'Failed to update member.';
        },
      });
    } else {
      this.memberService.createMember(this.formData).subscribe({
        next: (res) => {
          this.isSaving = false;
          this.successMessage = res?.message || 'Member created successfully!';
          this.closeModal();
          this.loadMembers();
        },
        error: (err) => {
          this.isSaving = false;
          this.errorMessage = err?.error?.message || 'Failed to create member.';
        },
      });
    }
  }

  confirmDelete(member: MemberModel): void {
    this.deletingMember = member;
    this.showDeleteConfirmModal = true;
  }

  cancelDelete(): void {
    this.deletingMember = null;
    this.showDeleteConfirmModal = false;
  }

  deleteMember(): void {
    if (!this.deletingMember) return;

    this.isDeleting = true;
    const memberId = this.deletingMember.id;

    this.memberService.deleteMember(memberId).subscribe({
      next: (res) => {
        this.isDeleting = false;
        this.successMessage = res?.message || 'Member deleted successfully!';
        this.cancelDelete();
        this.loadMembers();
      },
      error: (err) => {
        this.isDeleting = false;
        this.errorMessage = err?.error?.message || 'Failed to delete member.';
        this.cancelDelete();
      },
    });
  }
}

