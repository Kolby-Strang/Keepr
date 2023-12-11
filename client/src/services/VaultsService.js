import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
    async saveActiveKeepToVault(vaultId) {
        await api.post('api/vaultKeeps', { keepId: AppState.activeKeep.id, vaultId })
        AppState.activeKeep.kept++
    }
}


export const vaultsService = new VaultsService()