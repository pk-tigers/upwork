import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
})
export class NavigationComponent implements OnInit {
  activeIndex = 0;

  MENU_DATA = [
    { icon: "dashboard", text: "Dashboard", router_link: "" }, 
    { icon: "person_outline", text: "Profile", router_link: "" },
    { icon: "beach_access", text: "Time off", router_link: "" },
    { icon: "calendar_today", text: "Calendar", router_link: "" },
    { icon: "settings", text: "Settings", router_link: "" },
    { icon: "event_note", text: "Requests", router_link: "" }];

  constructor() { }

  ngOnInit(): void { }

  onItemClick(index: number) {
    this.activeIndex = index;
  }

  isMenuActive(index: number) {
    return this.activeIndex == index;
  }
}
