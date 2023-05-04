export interface InputPopupDataModel {
  title: string;
  description: string;
  buttons: ButtonPopupModel[];
  inputs: Dictionary<InputPopupModel>;
}

export interface ButtonPopupModel {
  type: ButtonTypes;
  text: string;
  onClick?: CallableFunction;
}

export enum ButtonTypes {
  PRIMARY,
  SECONDARY,
}

export interface InputPopupModel {
  type: string;
  placeholder?: string;
  selectOptions?: SelectOptionPopupModel[];
  value?: number | string;
}

export interface SelectOptionPopupModel {
  value: number | string;
  displayValue: string;
}

export interface Dictionary<T> {
  [Key: string]: T;
}
