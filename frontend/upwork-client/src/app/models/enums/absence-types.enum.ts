export enum AbsenceTypes {
  PaidTimeOff,
  UnpaidLeave,
  SickLeave,
}

export class AbsenceTypesHelper {
  static needApproval(absceneType: AbsenceTypes): boolean {
    if (absceneType == AbsenceTypes.SickLeave) return false;
    return true;
  }
}
