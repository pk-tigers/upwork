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
}
