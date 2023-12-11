import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { api } from "./AxiosService"

class KeepsService {

    async getAllKeepsIntoAppState() {
        const res = await api.get('api/keeps')
        AppState.allKeeps = res.data.map(pojo => new Keep(pojo))
    }
    setActiveKeep(keep) {
        AppState.activeKeep = keep
    }
    async getKeepByIdAndUpdateAppState(keepId) {
        const res = await api.get(`api/keeps/${keepId}`)
        const keep = new Keep(res.data)
        const targetIndex = AppState.allKeeps.findIndex(k => k.id == keep.id)
        AppState.allKeeps.splice(targetIndex, 1, keep)
        return keep
    }

}

export const keepsService = new KeepsService()