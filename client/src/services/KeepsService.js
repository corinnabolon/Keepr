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

  async getKeepByIdAndSetAsActive(keepId) {
    AppState.activeKeep = null
    const res = await api.get(`api/keeps/${keepId}`)
    logger.log("res.data", res.data)
    AppState.activeKeep = new Keep(res.data)
  }

  async createKeep(keepData) {
    const res = await api.post("api/keeps", keepData)
    logger.log(res.data)
    AppState.keeps.push(new Keep(res.data))
    //TODO make sure this will show up if I'm creating from my profile page, too
  }

  clearKeepData() {
    AppState.activeKeep = null,
      AppState.keeps.length = 0,
      AppState.vaultKeeps.length = 0,
      AppState.profileKeeps.length = 0
  }

}

export const keepsService = new KeepsService()