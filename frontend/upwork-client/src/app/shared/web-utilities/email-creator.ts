export class EmailCreator {
  public static createEmail(
    firstName: string,
    lastName: string,
    organizationName: string
  ) {
    return `${firstName.toLowerCase()}.${lastName.toLowerCase()}@${organizationName.toLowerCase()}.upwork.com.pl`;
  }
}
