<template>
    <div @click="openVaultKeepModal()" class="position-relative selectable box-shadow rounded">
        <img class="vaultKeep-img rounded" :src="vaultKeep.img" :alt="`Image for Keep ${vaultKeep.name}`">
        <div class="position-absolute t-0 w-100 h-100 d-flex align-items-end p-2">
            <p class="text-shadow fs-4 text-white mb-0 fw-bold">{{ vaultKeep.name }}</p>
        </div>
    </div>
</template>


<script>
import { Modal } from 'bootstrap';
import { VaultKeep } from '../models/VaultKeep';
import { vaultKeepsService } from '../services/VaultKeepsService';

export default {
    props: {
        vaultKeep: { type: VaultKeep, required: true }
    },
    setup(props) {
        function openVaultKeepModal() {
            vaultKeepsService.setActiveVaultKeep(props.vaultKeep)
            Modal.getOrCreateInstance('#vaultKeepDetails').show()
        }
        return { openVaultKeepModal }
    }
};
</script>


<style lang="scss" scoped>
.vaultKeep-img {
    width: 100%;
    object-fit: cover;
    object-position: center;
}
</style>