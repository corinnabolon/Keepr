<template>
  <div class="modal fade" id="createKeepModal" tabindex="-1" aria-labelledby="createKeepModal Label" aria-hidden="true">
    <div class="modal-dialog modal-md">
      <div class="modal-content">
        <div class="modal-header border-0 pb-0 d-flex align-items-center">
          <p class="mb-0 fw-bold text-theme-darkgray font-menu fs-3" id="createKeepModal Label">Add your keep</p>
          <button type="button" class="btn-close mb-0" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div>
          <form class="mt-3 font-descriptions text-theme-gray" @submit.prevent="createKeep">
            <div class="mb-4 mx-4">
              <input v-model="editableKeep.name" type="text" class="form-control fs-4" id="title" placeholder="Title..."
                required maxLength="255" minLength="2">
            </div>
            <div class="mb-4 mx-4">
              <input v-model="editableKeep.img" type="url" class="form-control fs-4" id="title" placeholder="Image URL..."
                required maxLength="1000">
            </div>
            <div class="mb-4 mx-4">
              <textarea v-model="editableKeep.description" required type="text" class="form-control fs-4" id="description"
                placeholder="Keep description..." maxLength="1000" minLength="3"></textarea>
            </div>
            <div class="text-end mt-3">
              <button type="submit" class="btn btn-theme-charcoal me-3 mb-3 fs-4">Create</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { onMounted, ref } from 'vue';
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";
import { useRoute } from "vue-router";

export default {
  setup() {
    let editableKeep = ref({})
    const route = useRoute();

    onMounted(() => {
      const createKeepModalElem = document.getElementById('createKeepModal')
      createKeepModalElem.addEventListener('hidden.bs.modal', function (event) {
        editableKeep.value = {}
      })
    })

    return {
      route,
      editableKeep,

      async createKeep() {
        try {
          const keepData = editableKeep.value
          await keepsService.createKeep(keepData, route)
          Pop.success("Keep created!")
          editableKeep.value = {}
          Modal.getOrCreateInstance("#createKeepModal").hide()
        } catch (error) {
          Pop.error(error)
        }
      },

    }
  }
};
</script>


<style lang="scss" scoped>
input {
  outline: 0;
  border: 0;
  border-bottom: 1px solid var(--theme-gray);
}

textarea {
  outline: 0;
  border: 0;
  border-bottom: 1px solid var(--theme-gray);
  height: 30dvh;
}
</style>