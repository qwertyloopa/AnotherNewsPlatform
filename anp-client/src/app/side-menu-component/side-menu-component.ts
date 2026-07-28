import {KeyValuePipe, NgTemplateOutlet} from '@angular/common';
import {ChangeDetectionStrategy, Component, Directive, signal} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {RouterLink} from '@angular/router';
import {TuiPortals, TuiPortalService, tuiProvide, TuiVCR} from '@taiga-ui/cdk';
import {
  TuiButton,
  TuiDataList,
  TuiDropdown,
  TuiIcon,
  TuiInput,
  TuiLink,
  TuiPopupService,
  TuiTitle,
} from '@taiga-ui/core';
import {
  TuiAvatar,
  TuiBadge,
  TuiBadgeNotification,
  TuiBreadcrumbs,
  TuiChevron,
  TuiDataListDropdownManager,
  TuiFade,
  TuiSwitch,
  TuiTabs,
} from '@taiga-ui/kit';
import {TuiCardLarge, TuiForm, TuiHeader, TuiNavigation} from '@taiga-ui/layout';

@Component({
  selector: 'app-side-menu-component',
  imports: [

    FormsModule,
    KeyValuePipe,
    NgTemplateOutlet,
    RouterLink,
    TuiAvatar,
    TuiBadge,
    TuiBadgeNotification,
    TuiBreadcrumbs,
    TuiButton,
    TuiCardLarge,
    TuiChevron,
    TuiDataList,
    TuiDataListDropdownManager,
    TuiDropdown,
    TuiFade,
    TuiForm,
    TuiHeader,
    TuiIcon,
    TuiInput,
    TuiLink,
    TuiNavigation,
    TuiSwitch,
    TuiTabs,
    TuiTitle,
    TuiVCR,

  ],
  templateUrl: './side-menu-component.html',
  styleUrl: './side-menu-component.css',
})
export class SideMenuComponent {}
