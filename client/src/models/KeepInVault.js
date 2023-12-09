import { Keep } from "./Keep.js";


export class KeepInVault extends Keep {
  constructor(data) {
    super(data);
    this.vaultKeepId = data.vaultKeepId  //The many-to-many ID (vaultKeep.id will match the keep.id)
    this.vaultId = data.vaultId
  }
}