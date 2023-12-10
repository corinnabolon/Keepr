import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {

  async getKeeps() {
    const res = await api.get("api/keeps");
    AppState.keeps = res.data.map((pojo) => new Keep(pojo));
    logger.log(AppState.keeps)
    //TODO: ask re the order they are coming in
  }

  setActiveKeep(keepId) {
    AppState.activeKeep = AppState.keeps.find(keep => keep.id == keepId);
    logger.log("ACTIVEKEEP from the Service!", AppState.activeKeep)
  }

  setActiveVaultKeep(keepId) {
    AppState.activeKeep = AppState.vaultKeeps.find(keep => keep.id == keepId);
    logger.log("ACTIVEKEEP from the Service!", AppState.activeKeep)
  }


  setActiveProfileKeep(keepId) {
    AppState.activeKeep = AppState.profileKeeps.find(keep => keep.id == keepId);
    logger.log("ACTIVEKEEP from the Service!", AppState.activeKeep)
  }


  async createKeep(keepData) {
    const res = await api.post("api/keeps", keepData)
    logger.log(res.data)
    AppState.keeps.push(new Keep(res.data))
  }

  clearKeepData() {
    AppState.activeKeep = null,
      AppState.keeps.length = 0,
      AppState.vaultKeeps.length = 0,
      AppState.profileKeeps.length = 0
  }

}

export const keepsService = new KeepsService()