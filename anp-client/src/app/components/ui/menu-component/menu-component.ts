import { Component } from '@angular/core';
import {TuiAvatar} from "@taiga-ui/kit";
import {TuiButton} from "@taiga-ui/core";
import {TuiHeaderComponent, TuiLogoComponent} from "@taiga-ui/layout";

@Component({
  imports: [TuiAvatar, TuiButton, TuiHeaderComponent, TuiLogoComponent],
  selector: 'app-menu-component',
  styleUrl: './menu-component.less',
  templateUrl: './menu-component.html',
})
export class MenuComponent {}
