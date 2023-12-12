import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard, authSettled } from '@bcwdev/auth0provider-client'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage')
  },
  {
    path: '/about',
    name: 'About',
    component: loadPage('AboutPage')
  },
  {
    path: '/account',
    name: 'Account',
    component: loadPage('AccountPage'),
    beforeEnter: authGuard
  },
  {
    path: "/profiles/:profileId",
    name: "Profile",
    component: loadPage("ProfilePage")
  },
  {
    path: "/vaults/:vaultId",
    name: "Vault",
    component: loadPage("VaultPage"),
    beforeEnter: authSettled
  }
]

export const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes,
  scrollBehavior(to, from, savedPosition) {
    if (to.hash) {

      return new Promise((resolve, reject) => {
        setTimeout(() => {
          resolve({
            el: to.hash,
            top: -10,
            behavior: 'smooth'
          })
        }, 1500)
      })
    }
  }
})
