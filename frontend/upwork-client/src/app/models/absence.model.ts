import { AbsenceType } from './enums/absence-type.enum';
import { ApprovalState } from './enums/approval-state.enum';

export interface Absence {
  id: string;
  fromDate: Date;
  toDate: Date;
  absenceType: AbsenceType;
  userId: string;
  userName?: string;
  approvalState: ApprovalState;
  timeOffSupervisorId?: string;
}
