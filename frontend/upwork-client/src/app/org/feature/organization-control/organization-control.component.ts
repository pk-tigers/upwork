import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PopupWithInputsComponent } from '../../../shared/ui/popup_with_inputs/popup-with-inputs.component';
import {
  ButtonPopupModel,
  ButtonTypes,
  InputPopupDataModel,
  InputPopupModel,
} from 'src/app/models/input-popup-data.model';
import { Dictionary } from 'src/app/models/dictionary.model';
import { RegisterModel } from 'src/app/models/register.model';

@Component({
  selector: 'app-organization-control',
  templateUrl: './organization-control.component.html',
  styleUrls: ['./organization-control.component.scss'],
})
export class OrganizationControlComponent {
  constructor(private dialog: MatDialog) {}

  openAddUserPopup(): void {
    const inputs: Dictionary<InputPopupModel> = {
      ['firstname']: { value: '', type: 'text', placeholder: 'Firstname' },
      ['lastname']: { value: '', type: 'text', placeholder: 'Lastname' },
      ['email']: { value: '', type: 'text', placeholder: 'E-mail' },
    };
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Create user',
        onClick: () => {
          for (const key in inputs) {
            console.log(inputs[key]);
          }
        },
      },
      { type: ButtonTypes.SECONDARY, text: 'Cancel' },
    ];

    const data: InputPopupDataModel = {
      title: 'Create user',
      description: 'Fill basic data:',
      inputs: inputs,
      buttons: buttons,
    };

    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  openDeleteUserPopup() {
    // Logika otwierania popupa dla "Delete User"
  }

  openBlockUserPopup() {
    // Logika otwierania popupa dla "Block User"
  }

  openResetPasswordPopup() {
    // Logika otwierania popupa dla "Reset User's Password"
  }

  openGrantPermissionsPopup() {
    // Logika otwierania popupa dla "Grant Permissions"
  }

  openSetSupervisorPopup() {
    // Logika otwierania popupa dla "Set Supervisor"
  }

  openOrganizationSettingsPopup() {
    const dialogRef = this.dialog.open(PopupWithInputsComponent, {
      data: {
        popupTitle: 'Organization Settings',
        popupInfo: 'Please, choose an option:',
        buttons: [
          {
            text: 'Change organization name',
            action: () => {
              this.openOrganizationNameChange();
            },
          },
          {
            text: 'Delete organization',
            action: () => {
              this.openDeleteOrganization();
            },
          },
        ],
        btnSecondaryText: 'Cancel',
      },
    });

    dialogRef.afterClosed().subscribe(result => {
      // Obsługa zamknięcia popupa
    });
  }

  openDeleteOrganization() {
    const dialogRef = this.dialog.open(PopupWithInputsComponent, {
      data: {
        popupTitle: 'Delete Organization',
        popupInfo: 'Do you want to confirm this operation?',
        btnPrimaryText: 'Confirm',
        btnSecondaryText: 'Cancel',
      },
    });

    dialogRef.afterClosed().subscribe(result => {
      // Obsługa zamknięcia popupa
    });
  }

  openOrganizationNameChange() {
    const dialogRef = this.dialog.open(PopupWithInputsComponent, {
      data: {
        popupTitle: 'Change Organization Name',
        popupInfo: 'Enter:',
        inputs: [{ placeholder: 'New organization name', value: '' }],
        btnPrimaryText: 'Continue',
        btnSecondaryText: 'Cancel',
      },
    });

    dialogRef.afterClosed().subscribe(result => {
      // Obsługa zamknięcia popupa
    });
  }

  handleAddUser(): void {
    // Logika dodawania użytkownika
  }
}
