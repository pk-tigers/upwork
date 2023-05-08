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
      ['TimeOffBeginningDate']: {
        value: '',
        type: 'date',
        placeholder: 'Select beginning date',
      },
      ['TimeOffEndDate']: {
        value: '',
        type: 'date',
        placeholder: 'Select end date',
      },
    };
    console.log(inputs);
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Submit',
        //onClick: () => this.createOrganization(inputs),
      },
    ];
    const data: InputPopupDataModel = {
      title: 'New time off request',
      description: '',
      inputs: inputs,
      buttons: buttons,
    };
    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }
}
