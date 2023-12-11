import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { api } from "./AxiosService.js"
import { KeepInVault } from "../models/KeepInVault.js"

class VaultsService {

  async getVaultById(vaultId) {
    AppState.activeVaultArray.length = 0
    const res = await api.get(`api/vaults/${vaultId}`)
    let newVault = new Vault(res.data)
    AppState.activeVault = newVault
    AppState.activeVaultArray.push(newVault)
    //TODO: find more efficient way than making this array of a single thing to pass down props so can use the image as a background
  }

  async getKeepsInVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    AppState.vaultKeeps = res.data.map((pojo) => new KeepInVault(pojo))
  }

  async createVault(vaultData) {
    if (vaultData.isPrivate == null) {
      vaultData.isPrivate = false
    }
    const res = await api.post("api/vaults", vaultData)
    AppState.myVaults.push(new Vault(res.data))
  }

  async editVault(vaultData) {
    if (vaultData.isPrivate == null) {
      vaultData.isPrivate = false
    }
    const res = await api.put(`api/vaults/${vaultData.id}`, vaultData)
    const updatedVault = new Vault(res.data)
    AppState.activeVault = updatedVault
    AppState.activeVaultArray.splice(0, 1, updatedVault)
    //TODO: see the above Todo re: this array
  }

  async destroyVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`)
    let vaultIndex = AppState.myVaults.findIndex((vault) => vault.id == vaultId)
    AppState.myVaults.splice(vaultIndex, 1)
  }

  clearVaultData() {
    AppState.activeVault = null;
    AppState.activeVaultArray.length = 0;
    AppState.vaultKeeps.length = 0;
    AppState.myVaults.length = 0;
  }



}

export const vaultsService = new VaultsService()