import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Vault } from '../models/Vault'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
      this.getUserVaultsAddAppState()
    } catch (err) {
      Pop.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async getUserVaultsAddAppState() {
    try {
      const res = await api.get('/account/vaults')
      AppState.activeUserVaults = res.data.map(v => new Vault(v))
    } catch (error) {
      Pop.error(error)
    }
  }
}

export const accountService = new AccountService()
