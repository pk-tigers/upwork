export interface AbsenceModel {
  id: string;
  fromDate: Date;
  toDate: Date;
  isActive: boolean;
  absenceTypeId: string;
  approvalState: number;
  timeOffSupervisorId: string;
}
