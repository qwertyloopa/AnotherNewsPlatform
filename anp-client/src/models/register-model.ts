import { FormControl, Validators } from '@angular/forms';

export class RegisterModel {
  username: FormControl = new FormControl<string>('', { nonNullable: true });
  password: FormControl = new FormControl<string>('', { nonNullable: true });
  passwordConfirmation: FormControl = new FormControl<string>('', { nonNullable: true });
  email: FormControl = new FormControl<string>('', { nonNullable: true });
}
