import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/shared/data-access/service/user.service';
import { Dictionary } from 'cypress/types/lodash';
import { PopupWithInputsComponent } from 'src/app/shared/ui/popup-with-inputs/popup-with-inputs.component';
import { MatDialog } from '@angular/material/dialog';
import {
  ButtonPopupModel,
  ButtonTypes,
  InputPopupDataModel,
  InputPopupModel,
} from 'src/app/models/input-popup-data.model';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent {
  photoPath = 'assets/images/default-avatar.png';
  profileForm: FormGroup;
  user: User | undefined;
  supervisor: string | undefined;
  companyName: string | undefined;
  vacation: number | undefined;
  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private router: Router,
    private dialog: MatDialog
  ) {
    this.profileForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });

    this.user = { firstName: 'John', lastName: 'Smith', email: 'email@com' };
    this.supervisor = 'Marek smith';
    this.companyName = 'Upwork';
    this.vacation = 50;
  }

  openChangeProfilePopup(): void {
    const inputs: Dictionary<InputPopupModel> = {
      ['firstName']: {
        value: this.user?.firstName,
        type: 'text',
        placeholder: 'Name',
      },
      ['lastName']: {
        value: this.user?.lastName,
        type: 'text',
        placeholder: 'Surname',
      },
      ['password']: {
        value: '',
        type: 'password',
        placeholder: 'New password',
      },
      ['repeatPassword']: {
        value: '',
        type: 'password',
        placeholder: 'Repeat password',
      },
    };
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Edit',
        onClick: () => this.openConfirmPopup(),
      },
      {
        type: ButtonTypes.SECONDARY,
        text: 'Cancel',
        onClick: () => this.dialog.closeAll(),
      },
    ];
    const data: InputPopupDataModel = {
      title: 'Change profile',
      description: '',
      inputs: inputs,
      buttons: buttons,
    };
    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }

  openConfirmPopup(): void {
    const inputs: Dictionary<InputPopupModel> = {
      ['password']: {
        value: '',
        type: 'password',
        placeholder: 'Current password',
      },
    };
    const buttons: ButtonPopupModel[] = [
      {
        type: ButtonTypes.PRIMARY,
        text: 'Confirm',
        onClick: () => console.log('A'),
      },
      {
        type: ButtonTypes.SECONDARY,
        text: 'Cancel',
        onClick: () => this.dialog.closeAll(),
      },
    ];
    const data: InputPopupDataModel = {
      title: 'Change profile',
      description: 'Please provide current password in order to proceed',
      inputs: inputs,
      buttons: buttons,
    };
    this.dialog.open(PopupWithInputsComponent, {
      data: data,
      panelClass: 'upwork-popup',
    });
  }
}
