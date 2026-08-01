import { TuiRoot, TuiButton } from '@taiga-ui/core';
import { Component } from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {TuiElasticContainer, TuiMainComponent, TuiNavigation} from '@taiga-ui/layout';

@Component({
  selector: 'app-root',
  imports: [TuiRoot, RouterOutlet, TuiMainComponent, TuiNavigation, TuiElasticContainer, TuiButton, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected sidebarOpen = false;

  toggleSidebar(): void {
    this.sidebarOpen = !this.sidebarOpen;
  }
}
