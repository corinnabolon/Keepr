<template>
  <div
    class="navbar navbar-expand-lg container-fluid justify-content-between bg-theme-beige text-theme-dracula-orchid font-menu box-shadow nav-height">
    <section class="row">
      <div class="col-5">
        <router-link :to="{ name: 'Home' }">
          <img src="../assets/img/Keeprlogo.png" alt="Keepr logo" title="Keepr logo" class="theme-img">
        </router-link>
      </div>
      <div class="col-3">
        <ul class="navbar-nav">
          <li v-if="account.id" class="dropup font-menu fw-bold fs-4">
            <!-- <a class="nav-link dropdown-toggle" href="#collapseExample" role="button" data-toggle="collapse"
              data-target="#collapseExample">
              Create
            </a> -->
            <!-- <ul class="dropdown-menu bg-theme-violet">
              <li><a class="dropdown-item menu-theme font-menu fw-bold fs-4" href="#" data-bs-toggle="modal"
                  data-bs-target="#createKeepModal">new keep</a>
              </li>
              <li><a class="dropdown-item font-menu menu-theme fw-bold fs-4" href="#" data-bs-toggle="modal"
                  data-bs-target="#createVaultModal">new
                  vault</a></li>
            </ul> -->
            <div class="collapse" id="collapseExample">
              <div class="card card-body">
                Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. Nihil anim
                keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident.
              </div>
            </div>
          </li>
        </ul>
      </div>
    </section>
    <div class="col-3">
      <Login />
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed } from 'vue';
import CreateKeepModal from "./CreateKeepModal.vue";
import CreateVaultModal from "./CreateVaultModal.vue";
import Login from './Login.vue';


export default {
  setup() {

    return {
      account: computed(() => AppState.account),
    }
  },
  components: { Login, CreateKeepModal, CreateVaultModal }
}
</script>


<style lang="scss" scoped>
.menu-theme {
  background-color: var(--theme-violet);
  border-bottom: 2px solid var(--theme-violet);
}

.menu-theme:hover {
  background-color: var(--theme-violet);
  border-bottom: 2px solid var(--theme-gray);
}

.theme-img {
  height: 66px;
}

.home {
  height: 100%;
}

.nav-height {
  height: 15dvh;
  width: 100%;
}
</style>