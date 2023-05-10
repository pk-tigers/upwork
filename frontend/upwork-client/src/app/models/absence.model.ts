import { AbsenceTypes } from './enums/absence-types.enum';

export interface AbsenceModel {
  id?: string;
  fromDate: Date;
  toDate: Date;
  isActive?: boolean;
  absenceTypeId?: string; //AbsenceTypes;
  approvalState?: number;
  timeOffSupervisorId?: string;
}
