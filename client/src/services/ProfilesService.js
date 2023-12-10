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
    logger.log("active profile", AppState.activeProfile)
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
      logger.log("profileId", profileId)
      const res = await api.get(`api/profiles/${profileId}/keeps`)
      AppState.profileKeeps = res.data.map((pojo) => new Keep(pojo))
      logger.log("profileKeeps", AppState.profileKeeps)
    } catch {
      logger.log("Could not get Keeps for this Profile.")
    }
  }

  async getActiveProfileVaults(profileId) {
    try {
      const res = await api.get(`api/profiles/${profileId}/vaults`)
      AppState.profileVaults = res.data.map((pojo) => new Vault(pojo))
      logger.log("profileVaults", AppState.profileVaults)
    } catch {
      logger.log("Could not get Vaults for this Profile.")
    }
  }

}

export const profilesService = new ProfilesService();