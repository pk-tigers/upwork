import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PopupComponent } from '../shared/ui/popup/popup.component';
import { PopupWithInputsComponent } from '../shared/ui/popup_with_inputs/popup-with-inputs.component';

@Component({
  selector: 'app-organization-control',
  templateUrl: './organization-control.component.html',
  styleUrls: ['./organization-control.component.css']
})
export class OrganizationControlComponent {


constructor(private dialog: MatDialog) {}

  openAddUserPopup(): void {
    const dialogRef = this.dialog.open(PopupWithInputsComponent, {
      data: {
        width: '900px',
        popupTitle: 'Add User',
        popupInfo: 'Please enter user details:',
        inputs: [
          { placeholder: 'Email', value: '' },
          { placeholder: 'Password', value: '' },
          { placeholder: 'Confirm Password', value: '' }
        ],
        btnPrimaryText: 'Continue',
        btnSecondaryText: 'Cancel',
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      // Obsługa zamknięcia popupa
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
          { text: 'Change organization name', action: () => { this.openOrganizationNameChange(); } },
          { text: 'Delete organization', action: () => { this.openDeleteOrganization(); }  }
        ],
        btnSecondaryText: 'Cancel',
      }
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
      }
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
        inputs: [
          { placeholder: 'New organization name', value: '' },
        ],
        btnPrimaryText: 'Continue',
        btnSecondaryText: 'Cancel',
      }
    });



    dialogRef.afterClosed().subscribe(result => {
      // Obsługa zamknięcia popupa
    });
  }

  handleAddUser(): void {
    // Logika dodawania użytkownika
  }


}
