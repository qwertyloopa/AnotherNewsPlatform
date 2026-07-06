import { Component } from '@angular/core';
import { LoginModel } from '../../models/login-model';
import { FormBuilder, FormsModule } from '@angular/forms';
import { parseJson } from '@angular/cli/src/utilities/json-file';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login-form-component',
  imports: [FormsModule, RouterLink],
  templateUrl: './login-form-component.html',
  styleUrl: './login-form-component.css',
})
export class LoginFormComponent {
  loginModel: LoginModel;
  constructor(private formBuilder: FormBuilder) {
    this.loginModel = new LoginModel();
  }

  login(): void {
    console.log(`Login attempted with ${this.loginModel.email}, ${this.loginModel.password}`);
  }
}
