import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { TuiButton, TuiInputDirective, TuiLabel, TuiTextfieldComponent, TuiTitle } from '@taiga-ui/core';
import { TuiCardLarge, TuiForm, TuiHeader } from '@taiga-ui/layout';

@Component({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    TuiButton,
    TuiCardLarge,
    TuiForm,
    TuiHeader,
    TuiInputDirective,
    TuiLabel,
    TuiTextfieldComponent,
    TuiTitle,
  ],
  selector: 'app-login-page-component',
  styleUrl: './login-page-component.less',
  templateUrl: './login-page-component.html',
})
export class LoginPageComponent {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });
}
