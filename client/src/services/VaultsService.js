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
}


export const vaultsService = new VaultsService()