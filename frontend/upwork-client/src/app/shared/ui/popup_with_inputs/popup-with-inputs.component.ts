import { Component, Input, OnInit, Inject } from '@angular/core';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
@Component({
  selector: 'app-popup-with-inputs',
  templateUrl: './popup-with-inputs.component.html',
  styleUrls: ['./popup-with-inputs.component.scss'],
})
export class PopupWithInputsComponent implements OnInit {
  closePopup() {
    this.dialogRef.close('primary');
  }

  constructor(
    public dialogRef: MatDialogRef<PopupWithInputsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.width = '600px';
    this.height = '300px';
    this.popupTitle = 'Popup';
    this.popupInfo = 'Confirm choice?';
    this.btnPrimaryText = 'Accept';
    this.btnSecondaryText = 'Decline';
    this.btnSecondaryFunction = this.closePopup;
    this.inputs = [];
    this.buttons = [];


  }

  ngOnInit(): void {

    this.width = this.data.width || '500px';
    this.height = this.data.height || '300px';
    this.popupTitle = this.data.popupTitle || 'Popup';
    this.popupInfo = this.data.popupInfo || 'Confirm choice?';
    this.btnPrimaryText = this.data.btnPrimaryText || undefined;
    this.btnSecondaryText = this.data.btnSecondaryText || 'Close';
    this.btnSecondaryFunction = this.data.btnSecondaryFunction || this.closePopup;
    this.inputs = this.data.inputs || [];
    this.buttons = this.data.buttons || [];
  }
  //Width of the popup with units, default 600px
  @Input() width: string;
  //Height of the popup with units, default 300px
  @Input() height: string;
  //Title of the popup, default "Popup"
  @Input() popupTitle: string;
  //Content of the popup, default "Confirm choice?"
  @Input() popupInfo: string;
  //Text of the primary button, default "Accept"
  @Input() btnPrimaryText: string;
  //Text of the secondary button, default "Decline"
  @Input() btnSecondaryText: string;
  //Function that will be executed on clicking primary button, takes no argument and returns none
  @Input() btnPrimaryFunction: (() => void | undefined) | undefined;
  //Function that will be executed on clicking secondary button, takes no argument and returns none, default remove popup
  @Input() btnSecondaryFunction: () => void;

  @Input() inputs: { placeholder: string, value: string }[];
  @Input() buttons: { text: string, action?: () => void }[];

}


