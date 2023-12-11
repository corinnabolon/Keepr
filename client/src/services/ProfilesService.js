import { AppState } from "../AppState.js"
import { Profile } from "../models/Profile.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { Keep } from "../models/Keep.js"
import { Vault } from "../models/Vault.js"
import { accountService } from "./AccountService.js"

class ProfilesService {

  async setActiveProfile(profileId) {
    const res = await api.get(`api/profiles/${profileId}`)
    AppState.activeProfile = new Profile(res.data)
    await this.getActiveProfileKeeps(profileId)
    if (AppState.account.id == profileId) {
      await accountService.getMyVaults()
    } else {
      await this.getActiveProfileVaults(profileId)
    }
  }

  clearData() {
    AppState.activeProfile = null
    AppState.keeps.length = 0
    AppState.profileKeeps.length = 0
    AppState.profileVaults.length = 0
  }

  async getActiveProfileKeeps(profileId) {
    try {
      const res = await api.get(`api/profiles/${profileId}/keeps`)
      AppState.profileKeeps = res.data.map((pojo) => new Keep(pojo))
    } catch {
      logger.log("Could not get Keeps for this Profile.")
    }
  }
  //TODO: ask if putting trycatches is OK like this in service and what else do to on the catch

  async getActiveProfileVaults(profileId) {
    try {
      const res = await api.get(`api/profiles/${profileId}/vaults`)
      AppState.profileVaults = res.data.map((pojo) => new Vault(pojo))
      logger.log("Active profile vaults:", AppState.profileVaults)
    } catch {
      logger.log("Could not get Vaults for this Profile.")
    }
  }

}

export const profilesService = new ProfilesService();