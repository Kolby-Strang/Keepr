import { Profile } from "./Profile";

export class Account extends Profile {
  constructor(data) {
    super(data)
    this.email = data.email
  }
}
