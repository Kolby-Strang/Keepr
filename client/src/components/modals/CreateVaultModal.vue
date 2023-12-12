<template>
    <ModalBase id="createVaultModal">
        <div class="p-3">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <p class="fs-2 fw-bold mb-0">Create Vault</p>
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
            <div class="d-flex justify-content-end">
                <div>
                    <div class="form-text">*Private vaults can only be seen by you</div>
                    <div class="form-check form-switch">
                        <input v-model="editable.isPrivate" minlength="1" maxlength="300" class="form-check-input"
                            type="checkbox" role="switch">
                        <label class="form-check-label">Make Vault Private?</label>
                    </div>
                    <button @click="createVault()" class="btn btn-dark w-100">Create Vault</button>
                </div>
            </div>
        </div>
    </ModalBase>
</template>


<script>
import { ref } from 'vue';
import ModalBase from './ModalBase.vue';
import Pop from '../../utils/Pop';
import { vaultsService } from '../../services/VaultsService';
import { Modal } from 'bootstrap';

export default {
    setup() {
        // VARIABLES
        const editable = ref({ isPrivate: false })
        // FUNCTIONS
        async function createVault() {
            try {
                await vaultsService.createVault(editable.value)
                editable.value = { isPrivate: false }
                Pop.success("Vault Created!")
                Modal.getOrCreateInstance('#createVaultModal').hide()
            } catch (error) {
                Pop.error(error)
            }
        }
        return { editable, createVault };
    },
    components: { ModalBase }
};
</script>


<style lang="scss" scoped>
.form-text {
    font-size: x-small;
}
</style>