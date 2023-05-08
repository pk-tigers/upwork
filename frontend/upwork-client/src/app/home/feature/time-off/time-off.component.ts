import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Dictionary } from 'cypress/types/lodash';
import {
  ButtonPopupModel,
  ButtonTypes,
  InputPopupDataModel,
  InputPopupModel,
} from 'src/app/models/input-popup-data.model';
import { PopupWithInputsComponent } from 'src/app/shared/ui/popup_with_inputs/popup-with-inputs.component';

@Component({
  selector: 'app-time-off',
  templateUrl: './time-off.component.html',
  styleUrls: ['./time-off.component.scss'],
})
export class TimeOffComponent {
  time_off_days = 21; //TODO: to be replaced with value from db

  constructor(private dialog: MatDialog) {}

  openNewRequestPopup(): void {
    const inputs: Dictionary<InputPopupModel> = {
      ['TimeOffNote']: {
        value: '',
        type: 'text',
        placeholder: 'Add a note',
      },
      ['TimeOffBeginningDate']: {
        value: '',
        type: 'date',
        placeholder: 'Enter beginning date',
      },
      ['TimeOffEndDate']: {
        value: '',
        type: 'date',
        placeholder: 'Enter end date',
      },
      ['TimeOffOptions']: {
        value: '',
        type: 'select',
        placeholder: 'Select type of Time off',
        selectOptions: [
          { value: 'paid-time-off', displayValue: 'Paid time off' },
          { value: 'sick-leave', displayValue: 'Sick leave' },
          { value: 'unpaid-leave', displayValue: 'Unpaid leave' },
        ],
      },
    };
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Submit',
        // onClick: () => this.createTimeOffRequest(inputs),
        onClick: () => this.createTimeOffRequest(inputs),
      },
    ];
    const data: InputPopupDataModel = {
      title: 'New time off request',
      description: 'Fill basic data about your request:',
      inputs: inputs,
      buttons: buttons,
    };
    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  createTimeOffRequest(inputs: Dictionary<InputPopupModel>): void {
    //TODO: add logic about creating new request by user
    for (const key in inputs) {
      console.log(inputs[key]);
    }
  }
}
