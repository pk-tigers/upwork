import { Component, EventEmitter, Input, Output } from '@angular/core';

export interface sharedTableData {
  cols?: string[];
  actions?: sf[];
}

export interface sf {
  icon: string;
  func: CallableFunction;
}

@Component({
  selector: 'app-shared-table',
  templateUrl: './shared-table.component.html',
  styleUrls: ['./shared-table.component.scss'],
})
export class SharedTableComponent {
  @Input() headers!: string[];
  @Input() data!: sharedTableData[];
  @Input() currentPage!: number;
  @Input() totalNumberOfPages!: number;

  @Output() prevPageEvent = new EventEmitter<number>();
  @Output() nextPageEvent = new EventEmitter<number>();

  prevPage() {
    if (this.currentPage > 0) this.prevPageEvent.emit(--this.currentPage);
  }

  nextPage() {
    if (this.currentPage < this.totalNumberOfPages)
      this.nextPageEvent.emit(++this.currentPage);
  }
}
