import { Component, inject } from '@angular/core';
import {TuiAvatar} from "@taiga-ui/kit";
import {TuiButton} from "@taiga-ui/core";
import {TuiHeaderComponent, TuiLogoComponent} from "@taiga-ui/layout";
import { UserService } from '../../../services/user-service';
import { RouterLink } from '@angular/router';

@Component({
  imports: [TuiAvatar, TuiButton, TuiHeaderComponent, TuiLogoComponent, RouterLink],
  selector: 'app-menu-component',
  styleUrl: './menu-component.less',
  templateUrl: './menu-component.html',
})
export class MenuComponent {
  private readonly userService: UserService = inject(UserService);
}
