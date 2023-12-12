import { AppState } from "../AppState"
import { VaultKeep } from "../models/VaultKeep"
import { api } from "./AxiosService"

class VaultKeepsService {
    async getVaultKeepsByVaultId(vaultId) {
        AppState.activeVaultKeeps = []
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        const vaultKeeps = res.data.map(vK => new VaultKeep(vK))
        AppState.activeVaultKeeps = vaultKeeps
        return vaultKeeps
    }
    async setActiveVaultKeep(vaultKeep) {
        AppState.activeVaultKeep = vaultKeep
    }
    async destroyVaultKeep(vaultKeepId) {
        await api.delete(`api/vaultKeeps/${vaultKeepId}`)
        const targetIndex = AppState.activeVaultKeeps.findIndex(vK => vK.vaultKeepId == vaultKeepId)
        AppState.activeVaultKeeps.splice(targetIndex, 1)
    }
}


export const vaultKeepsService = new VaultKeepsService()