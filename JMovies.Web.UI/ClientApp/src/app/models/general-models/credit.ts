import { CreditRoleType } from "./credit-role-type-enum";
import { Person } from "./person";

export class Credit{
    person: Person;
    roleType: CreditRoleType;
}