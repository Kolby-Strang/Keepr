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
    async getKeepsByProfileId(profileId, doesUserOwn) {
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        const keeps = res.data.map(k => new Keep(k))
        if (doesUserOwn) {
            AppState.activeUserKeeps = keeps
        }
        return keeps
    }
    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        const keep = new Keep(res.data)
        AppState.activeUserKeeps.push(keep)
        AppState.allKeeps.push(keep)
    }

}

export const keepsService = new KeepsService()