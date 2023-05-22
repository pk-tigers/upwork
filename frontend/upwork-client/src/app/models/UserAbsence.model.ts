import { AbsenceType } from './enums/absence-type.enum';

export interface UserAbsence {
  id: string;
  organizationId: string;
  firstName: string;
  lastName: string;
  fromDate: Date;
  toDate: Date;
  absenceType: AbsenceType;
}
