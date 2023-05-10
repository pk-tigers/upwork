export enum AbsenceTypes {
  PaidTimeOff,
  UnpaidLeave,
  SickLeave,
  MaternityLeave,
}

export class AbsenceTypesHelper {
  static needApproval(absceneType: AbsenceTypes): boolean {
    if (absceneType == AbsenceTypes.SickLeave) return false;
    return true;
  }
}
