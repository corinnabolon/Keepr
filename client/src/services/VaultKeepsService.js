import { AppState } from "../AppState.js";
import { KeepInVault } from "../models/KeepInVault.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";


class VaultKeepsService {

  async addToVault(vaultId, routename) {
    const keepId = AppState.activeKeep.id
    let vaultKeepData = {}
    if (routename == "Home") {
      vaultKeepData = AppState.keeps.find(keep => keep.id == keepId)
    } else if (routename == "Vault") {
      vaultKeepData = AppState.vaultKeeps.find(keep => keep.id == keepId)
    } else {
      vaultKeepData = AppState.profileKeeps.find(keep => keep.id == keepId)
    }
    vaultKeepData.vaultId = vaultId
    vaultKeepData.keepId = vaultKeepData.id
    logger.log("vaultKeepData after adding vaultId and keepId", vaultKeepData)
    //TODO: check all account's current vaultkeeps to make sure not duplicating--back end won't allow it--or get rid of "unique" on the back end OR figure out how to throw better error message
    const res = await api.post("api/vaultkeeps", vaultKeepData)
    logger.log("After posted", res.data)
    vaultKeepData.vaultKeepId = res.data.id
    const newVaultKeep = new KeepInVault(vaultKeepData)
    AppState.activeKeep.kept++,
      logger.log("KeepInVault", newVaultKeep)
  }

  async removeFromVault(keepId) {
    const vaultKeepToDelete = AppState.vaultKeeps.find(keep => keep.id == keepId)
    if (!vaultKeepToDelete) {
      return
    }
    let vaultKeepId = vaultKeepToDelete.vaultKeepId
    const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    logger.log(res.data)
    const vaultKeepIndex = AppState.vaultKeeps.findIndex(vaultkeep => vaultkeep.id == keepId)
    AppState.vaultKeeps.splice(vaultKeepIndex, 1)
  }

}

export const vaultKeepsService = new VaultKeepsService();