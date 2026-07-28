import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  email: string = '';
  password: string = '';
  rememberMe: boolean = false;
  showPassword: boolean = false;
  isLoading: boolean = false;
  errorMessage: string = '';

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  onLogin(): void {
    if (!this.email || !this.password) {
      this.errorMessage = 'Please fill in both email and password.';
      return;
    }
    
    this.errorMessage = '';
    this.isLoading = true;

    // Simulated API authentication call
    setTimeout(() => {
      this.isLoading = false;
      alert(`Welcome back to FinCircle! Logged in as ${this.email}`);
    }, 1200);
  }
}
