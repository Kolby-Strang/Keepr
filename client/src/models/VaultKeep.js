import { Keep } from "./Keep";

export class VaultKeep extends Keep {
    constructor(data) {
        super(data)
        this.vaultId = data.vaultId
        this.vaultKeepId = data.vaultKeepId
    }
}