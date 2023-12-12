import { Profile } from "../models/Profile"
import { api } from "./AxiosService"

class ProfilesService {
    async getProfileByProfileId(profileId) {
        const res = await api.get(`api/profiles/${profileId}`)
        return new Profile(res.data)
    }
}


export const profilesService = new ProfilesService()