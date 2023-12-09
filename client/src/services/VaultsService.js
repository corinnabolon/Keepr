import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { Keep } from "../models/Keep.js"

class VaultsService {

  async getVaultById(vaultId) {
    AppState.activeVaultArray.length = 0
    const res = await api.get(`api/vaults/${vaultId}`)
    let newVault = new Vault(res.data)
    AppState.activeVault = newVault
    AppState.activeVaultArray.push(newVault)
    //TODO: find more efficient way than making this array of a single thing to pass down props so can use the image as a background
    this.getKeepsInVault(vaultId);
  }

  async getKeepsInVault(vaultId) {
    try {
      const res = await api.get(`api/vaults/${vaultId}/keeps`)
      logger.log("Got keeps for this vault", res.data)
      AppState.vaultKeeps = res.data.map((pojo) => new Keep(pojo))
    } catch (error) {
      logger.log(`Could not get keeps for the vault: ${vaultId}`)
    }
  }

  clearVaultData() {
    AppState.activeVault = null;
    AppState.activeVaultArray.length = 0;
    AppState.vaultKeeps.length = 0;
  }



}

export const vaultsService = new VaultsService()