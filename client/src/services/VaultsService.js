import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
import { Vault } from '../models/Vault'

class VaultsService {
    async saveActiveKeepToVault(vaultId) {
        await api.post('api/vaultKeeps', { keepId: AppState.activeKeep.id, vaultId })
        AppState.activeKeep.kept++
    }
    async getVaultsByProfileId(profileId, doesUserOwn) {
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        const vaults = res.data.map(v => new Vault(v))
        if (doesUserOwn) {
            AppState.activeUserVaults = vaults
        }
        return vaults
    }
    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        AppState.activeUserVaults.push(new Vault(res.data))
    }
    async getVaultById(vaultId) {
        AppState.activeVault = {}
        const res = await api.get(`api/vaults/${vaultId}`)
        const vault = new Vault(res.data)
        AppState.activeVault = vault
        return vault
    }
    async destroyVault(vaultId) {
        await api.delete(`api/vaults/${vaultId}`)
        const targetIndex = AppState.activeUserVaults.findIndex(v => v.id == vaultId)
        AppState.activeUserVaults.splice(targetIndex, 1)
    }
    async editVault(vaultData) {
        const res = await api.put(`api/vaults/${vaultData.id}`, vaultData)
        const vault = new Vault(res.data)
        AppState.activeVault = vault
        const targetIndex = AppState.activeUserVaults.findIndex(v => v.id == vault.id)
        AppState.activeUserVaults.splice(targetIndex, 1, vault)
    }
}


export const vaultsService = new VaultsService()