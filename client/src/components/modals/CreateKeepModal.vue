<template>
    <ModalBase id="createKeepModal">
        <div class="p-3">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <p class="fs-2 fw-bold mb-0">Create Keep</p>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="mb-3">
                <input v-model="editable.name" minlength="1" maxlength="25" required type="text" class="form-control"
                    placeholder="Title...">
            </div>
            <div class="mb-3">
                <input v-model="editable.img" minlength="1" maxlength="1000" required type="text" class="form-control"
                    placeholder="Image URL...">
            </div>
            <div class="mb-4">
                <textarea v-model="editable.description" minlength="1" maxlength="300" class="form-control"
                    placeholder="Description..." rows="7"></textarea>
            </div>
            <div class="d-flex justify-content-end">
                <div>
                    <button @click="createKeep()" class="btn btn-dark w-100">Create Keep</button>
                </div>
            </div>
        </div>
    </ModalBase>
</template>


<script>
import { ref } from 'vue';
import ModalBase from './ModalBase.vue';
import Pop from '../../utils/Pop';
import { keepsService } from '../../services/KeepsService';
import { Modal } from 'bootstrap';

export default {
    setup() {
        const editable = ref({})

        function createKeep() {
            try {
                keepsService.createKeep(editable.value)
                editable.value = {}
                Modal.getOrCreateInstance('#createKeepModal').hide()
                Pop.success('Keep Created!')
            } catch (error) {
                Pop.error(error)
            }
        }
        return { editable, createKeep };
    },
    components: { ModalBase }
};
</script>


<style lang="scss" scoped></style>