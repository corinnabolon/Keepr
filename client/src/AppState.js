import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],
  /** @type {import('./models/Keep.js').Keep | null} */
  activeKeep: null,
  /** @type {import('./models/Vault.js').Vault | null} */
  myVaults: [],
  /** @type {import('./models/Profile.js').Profile | null} */
  activeProfile: null,
  /** @type {import('./models/Profile.js').Profile[]} */
  profileKeeps: [],
  /** @type {import('./models/Vault.js').Vault[]} */
  profileVaults: [],
  /** @type {import('./models/Vault.js').Vault | null} */
  activeVault: null,
  /** @type {import('./models/Vault.js').Vault[] */
  vaultKeeps: [],
  /** @type Boolean */
  wantsToDeleteKeeps: false,
  /** @type Boolean */
  wantsToDeleteVaults: false,
  /** @type Boolean */
  wantsToEditVault: false,
  /** @type Boolean */
  showMobileLoginMenu: false,
  /** @type Boolean */
  mobileShowingLinks: false,
})
