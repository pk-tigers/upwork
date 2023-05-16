import { AbsenceType } from './enums/absence-type.enum';
import { ApprovalState } from './enums/approval-state.enum';

export interface Absence {
  id?: string;
  fromDate: Date;
  toDate: Date;
  absenceType: AbsenceType;
  userId?: string;
  userName?: string;
<<<<<<< HEAD
  approvalState?: ApprovalState;
=======
  approvalState: ApprovalState;
>>>>>>> 48589fe41a4d6a9d5dcdc3979002e99551ec5238
  timeOffSupervisorId?: string;
}
