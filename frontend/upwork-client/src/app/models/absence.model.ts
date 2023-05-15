import { AbsenceType } from './enums/absence-type.enum';
import { AbsenceTypes } from './enums/absence-types.enum';
import { ApprovalState } from './enums/approval-state.enum';

export interface Absence {
  id?: string;
  fromDate: Date;
  toDate: Date;
  absenceType: AbsenceTypes;
  userId?: string;
  userName?: string;
  approvalState?: ApprovalState;
  timeOffSupervisorId?: string;
}
