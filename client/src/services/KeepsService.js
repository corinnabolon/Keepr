import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {

  async getKeeps() {
    const res = await api.get("api/keeps");
    AppState.keeps = res.data.map((pojo) => new Keep(pojo));
    logger.log(AppState.keeps)
  }

  async setActiveKeep(keepId) {
    AppState.activeKeep = AppState.keeps.find(keep => keep.id == keepId);
  }

}

export const keepsService = new KeepsService()