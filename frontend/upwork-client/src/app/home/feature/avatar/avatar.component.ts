import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-avatar',
  templateUrl: './avatar.component.html',
  styleUrls: ['./avatar.component.scss'],
})
export class AvatarComponent implements OnInit {
  @Input()
  public photoUrl: string | undefined;

  @Input()
  public firstName: string | undefined;

  @Input()
  public lastName: string | undefined;

  public showInitials = false;
  public initials: string | undefined;
  public circleColor: string | undefined;

  private colors = [
    '#2947A6', // Deep Blue
    '#2ECC71', // Emerald Green
    '#5F27CD', // Royal Purple
    '#FFC400', // Golden Yellow
    '#008080', // Teal Blue
    '#9683EC', // Lavender Purple
    '#DC143C', // Ruby Red
    '#082567', // Sapphire Blue
    '#98FB98', // Mint Green
    '#00FFFF', // Aqua Blue
    '#800000', // Maroon Red
    '#228B22', // Forest Green
    '#DA70D6', // Orchid Pink
    '#FFD700', // Sunshine Yellow
    '#000080', // Navy Blue
    '#FFDAB9', // Peachy Pink
    '#8B0000', // Dark Crimson
    '#808000', // Olive Green
    '#C8A2C8', // Lilac Purple
    '#FF6F61', // Coral Pink
  ];
  hashCode(str: string | undefined): number {
    let hash = 0;
    if (str) {
      for (let i = 0; i < str.length; i++) {
        const char = str.charCodeAt(i);
        hash = (hash << 5) - hash + char;
        hash = hash & hash;
      }
    } else {
      return hash;
    }
    return Math.abs(hash) % this.colors.length;
  }
  ngOnInit() {
    if (!this.photoUrl) {
      this.showInitials = true;
      if (this.firstName && this.lastName) {
        const firstNameInitial = this.firstName?.charAt(0).toUpperCase();
        const lastNameInitial = this.lastName?.charAt(0).toUpperCase();
        this.initials = firstNameInitial + lastNameInitial;
      } else {
        this.initials = 'UU';
        this.firstName = 'Unknown';
        this.lastName = 'User';
      }
      this.circleColor =
        this.colors[this.hashCode(this.firstName + this.lastName)];
    }
  }
}
