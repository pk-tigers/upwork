import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SharedTableData } from 'src/app/models/shared-table-data.model';

@Component({
  selector: 'app-shared-table',
  templateUrl: './shared-table.component.html',
  styleUrls: ['./shared-table.component.scss'],
})
export class SharedTableComponent {
  @Input() caption!: string;
  @Input() headers!: string[];
  @Input() data!: SharedTableData[];
  @Input() currentPage!: number;
  @Input() totalNumberOfPages!: number;

  @Output() prevPageEvent = new EventEmitter<number>();
  @Output() nextPageEvent = new EventEmitter<number>();

  prevPage() {
    if (this.currentPage > 0) this.prevPageEvent.emit(--this.currentPage);
  }

  nextPage() {
    if (this.currentPage < this.totalNumberOfPages - 1)
      this.nextPageEvent.emit(++this.currentPage);
  }
}
