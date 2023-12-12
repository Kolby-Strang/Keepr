<template>
    <ModalBase id="editVaultModal">
        <div class="p-3">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <p class="fs-2 fw-bold mb-0">Edit Vault</p>
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
                        <input v-model="editable.isPrivate" required class="form-check-input" type="checkbox" role="switch">
                        <label class="form-check-label">Make Vault Private?</label>
                    </div>
                    <button @click="editVault()" class="btn btn-dark w-100">Edit Vault</button>
                </div>
            </div>
        </div>
    </ModalBase>
</template>


<script>
import { computed, ref, watch } from 'vue';
import ModalBase from './ModalBase.vue';
import { AppState } from '../../AppState';
import { vaultsService } from '../../services/VaultsService';
import Pop from '../../utils/Pop';
import { Modal } from 'bootstrap';

export default {
    setup() {
        // VARIABLES
        const editable = ref({})
        const watchableVault = computed(() => AppState.activeVault)
        // FUNCTIONS
        async function editVault() {
            try {
                vaultsService.editVault(editable.value)
                Pop.success('Vault Edited!')
                Modal.getOrCreateInstance('#editVaultModal').hide()
            }
            catch (error) {
                Pop.error(error);
            }
        }
        // LIFECYCLE
        watch(watchableVault, () => {
            editable.value = { ...watchableVault.value }
        }, { immediate: true })
        return { editVault, editable };
    },
    components: { ModalBase }
};
</script>


<style lang="scss" scoped></style>