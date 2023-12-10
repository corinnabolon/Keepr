import { AppState } from "../AppState.js";
import { KeepInVault } from "../models/KeepInVault.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";


class VaultKeepsService {

  async addToVault(vaultId) {
    const keepId = AppState.activeKeep.id
    const vaultKeepData = AppState.keeps.find(keep => keep.id == keepId)
    vaultKeepData.vaultId = vaultId
    vaultKeepData.keepId = vaultKeepData.id
    logger.log("vaultKeepData after adding vaultId and keepId", vaultKeepData)
    //TODO: check all account's current vaultkeeps to make sure not duplicating--back end won't allow it--or get rid of "unique" on the back end OR figure out how to throw better error message
    const res = await api.post("api/vaultkeeps", vaultKeepData)
    logger.log("After posted", res.data)
    vaultKeepData.vaultKeepId = res.data.id
    const newVaultKeep = new KeepInVault(vaultKeepData)
    logger.log("KeepInVault", newVaultKeep)
  }

}

export const vaultKeepsService = new VaultKeepsService();