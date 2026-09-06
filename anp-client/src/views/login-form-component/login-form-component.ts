import { Component } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import { RouterLink } from '@angular/router';
import {TuiCard, TuiElasticContainer, TuiForm, TuiHeader} from "@taiga-ui/layout";
import {TuiButton, TuiError, TuiIcon, TuiInput, TuiLabel, TuiTextfieldComponent} from "@taiga-ui/core";
import {TuiAnimated} from "@taiga-ui/cdk";

@Component({
  selector: 'app-login-form-component',
  imports: [FormsModule, RouterLink, TuiCard, TuiForm, TuiHeader, TuiButton, TuiIcon, TuiElasticContainer, TuiTextfieldComponent, TuiLabel, ReactiveFormsModule, TuiAnimated, TuiInput, TuiError],
  templateUrl: './login-form-component.html',
  styleUrl: './login-form-component.css',
})
export class LoginFormComponent {
  protected readonly form: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  login(): void {
    console.log(`Login attempted with ${this.form.get("email")}, ${this.form.get("password")}`);
  }
}
