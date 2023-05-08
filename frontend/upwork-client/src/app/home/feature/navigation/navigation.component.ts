import { Component } from '@angular/core';
import { UserService } from 'src/app/shared/data-access/user.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
})
export class NavigationComponent {
  public activeIndex = 0;
  public logoutActive = false;

  MENU_DATA = [
    { icon: 'dashboard', text: 'Dashboard', router_link: '' },
    { icon: 'person_outline', text: 'Profile', router_link: '' },
    { icon: 'beach_access', text: 'Time off', router_link: '' },
    { icon: 'calendar_today', text: 'Calendar', router_link: 'calendar' },
    { icon: 'settings', text: 'Settings', router_link: '' },
    { icon: 'event_note', text: 'Requests', router_link: '' },
    {
      icon: 'business',
      text: 'Organization Control',
      router_link: '/organization-control',
    },
  ];

  constructor(public userService: UserService) {}

  public onItemClick(index: number) {
    this.activeIndex = index;
  }

  public isMenuActive(index: number) {
    return this.activeIndex == index;
  }

  public logout(): void {
    this.logoutActive = true;
    this.userService.logout();
    window.location.reload();
  }
}
