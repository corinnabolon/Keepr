import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Vault } from "../models/Vault.js"
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getMyVaults() {
    const res = await api.get("account/vaults")
    AppState.myVaults = res.data.map((pojo) => new Vault(pojo))
    logger.log("My vaults", AppState.myVaults)
  }

  async editAccount(accountData) {
    const res = await api.put("account", accountData)
    AppState.account = new Account(res.data)
  }
}

export const accountService = new AccountService()
