import { ModelBase } from "./ModelBase"

export class Profile extends ModelBase {
    constructor(data) {
        super(data)
        this.name = data.name
        this.picture = data.picture
        this.coverImg = data.coverImg
    }
}