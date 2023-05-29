import { User } from 'src/app/models/user.model';

export class SupervisorsSort {
  static sortAlphabeticallyFirtnameAndLastName(users: User[]): void {
    users.sort((a, b) => {
      const firstNameComparison = (a.firstName ?? '').localeCompare(
        b.firstName ?? ''
      );
      if (firstNameComparison !== 0) {
        return firstNameComparison;
      }
      return (a.lastName ?? '').localeCompare(b.lastName ?? '');
    });
  }
}
