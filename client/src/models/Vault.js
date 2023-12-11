import { ModelBase } from "./ModelBase";

export class Vault extends ModelBase {
    constructor(data) {
        super(data)
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.isPrivate = data.isPrivate
        this.creator = data.creator
    }
}