import { ModelBase } from "./ModelBase";

export class Keep extends ModelBase {
    constructor(data) {
        super(data)
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.views = data.views
        this.kept = data.kept
        this.creator = data.creator
    }
}
