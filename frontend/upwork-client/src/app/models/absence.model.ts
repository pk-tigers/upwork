<<<<<<< HEAD
import { AbsenceTypes } from './enums/absence-types.enum';

export interface AbsenceModel {
  id?: string;
  fromDate: Date;
  toDate: Date;
  isActive?: boolean;
  absenceTypeId?: string; //AbsenceTypes;
  approvalState?: number;
=======
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
>>>>>>> dev
  timeOffSupervisorId?: string;
}
