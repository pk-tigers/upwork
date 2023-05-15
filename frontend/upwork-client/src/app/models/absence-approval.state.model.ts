import { ApprovalState } from './enums/approval-state.enum';

export interface AbsenceApprovalState {
  absenceId: string;
  approvalState: ApprovalState;
}
