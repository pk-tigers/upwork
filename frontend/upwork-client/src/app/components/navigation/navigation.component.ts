import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
})
export class NavigationComponent implements OnInit {
  activeIndex = 0;

  constructor() {}

  ngOnInit(): void {}

  onItemClick(index: number) {
    this.activeIndex = index;
  }

  /*

  addActiveClassToClickedListItem() {
    const navItems = document.querySelectorAll('.nav-item');
    navItems.forEach(item => {
      item.addEventListener('click', event => {
        event.preventDefault();
        navItems.forEach(navItem => navItem.classList.remove('active'));
        item.classList.add('active');
        const url = item.querySelector('.nav-link').getAttribute('routerLink');
        if (url) {
          this.router.navigateByUrl(url);
        } else {
          this.location.back();
        }
      });
    });
  }*/
}
