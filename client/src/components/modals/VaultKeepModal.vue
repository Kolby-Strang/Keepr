<template>
    <ModalBase id="vaultKeepDetails" size="modal-xl">
        <div v-if="vaultKeep.vaultKeepId" class="row">
            <div class="col-12 col-md-6 pe-md-0">
                <img class="keep-img rounded-md-start rounded-top" :src="vaultKeep.img"
                    onerror="this.src='https://cdn.vectorstock.com/i/preview-1x/65/30/default-image-icon-missing-picture-page-vector-40546530.jpg'"
                    :alt="`Image for keep ${vaultKeep.name}`">
            </div>
            <div class="col-12 col-md-6 ps-md-0">
                <div class="info-container">

                    <div class="d-flex align-items-center pe-4">
                        <i class="mdi mdi-eye-outline fs-4 m-1"></i>
                        {{ vaultKeep.views }}
                        <i class="mdi mdi-alpha-k-box-outline fs-4 m-1 ms-3"></i>
                        {{ vaultKeep.kept }}
                    </div>
                    <div class="pe-4">
                        <p class="fs-1 text-dark fw-bold text-center">{{ vaultKeep.name }}</p>
                        <p>{{ vaultKeep.description }}</p>
                    </div>
                    <div class="row w-100 pe-4 pe-sm-2 pe-md-1">
                        <div class="col-12 col-sm-6 col-md-12 col-lg-5 p-0 d-flex align-items-end justify-content-center">
                            <div v-if="activeUserVaults.findIndex(v => v.id == vaultKeep.vaultId) != -1">
                                <button @click="destroyVaultKeep()" class="btn text-primary grey-underline fs-5">
                                    <i class="mdi mdi-cancel"></i> Remove
                                </button>
                            </div>
                        </div>
                        <div class="col-12 col-sm-6 col-md-12 col-lg-7 p-0 mt-2 mt-sm-0 mt-md-2 mt-lg-0">
                            <router-link @click="dismissModal()"
                                :to="{ name: 'ProfilePage', params: { 'profileId': vaultKeep.creator.id } }"
                                class="d-flex justify-content-center align-items-center">
                                <img class="profile-img" :src="vaultKeep.creator.picture"
                                    onerror="this.src='https://tr.rbxcdn.com/70108dc7da4e002c8e5d2c1dcf0825fb/420/420/Hat/Png'"
                                    :alt="vaultKeep.creator.name">
                                <p class="mb-0 ms-1 d-inline text-dark fw-bold">{{ vaultKeep.creator.name.length > 15 ?
                                    vaultKeep.creator.name.substring(0, 12) + '...' : vaultKeep.creator.name }}</p>
                            </router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ModalBase>
</template>


<script>
import { computed } from 'vue';
import ModalBase from './ModalBase.vue';
import { AppState } from '../../AppState';
import Pop from '../../utils/Pop';
import { Modal } from 'bootstrap';
import { vaultKeepsService } from '../../services/VaultKeepsService';
export default {
    setup() {
        const vaultKeep = computed(() => AppState.activeVaultKeep)
        const account = computed(() => AppState.account)
        const activeUserVaults = computed(() => AppState.activeUserVaults)
        async function destroyVaultKeep() {
            try {
                const yes = await Pop.confirm()
                if (!yes) {
                    Pop.toast('Cancelled')
                    return
                }
                await vaultKeepsService.destroyVaultKeep(vaultKeep.value.vaultKeepId)
                Pop.success(`${vaultKeep.value.name} removed from vault`)
                vaultKeepsService.setActiveVaultKeep({})
                Modal.getOrCreateInstance('#vaultKeepDetails').hide()
            } catch (error) {
                Pop.error(error)
            }
        }
        function dismissModal() {
            Modal.getOrCreateInstance('#vaultKeepDetails').hide()
        }
        return { vaultKeep, account, destroyVaultKeep, activeUserVaults, dismissModal }
    },
    components: { ModalBase }
};
</script>


<style lang="scss" scoped>
.keep-img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    aspect-ratio: 9/10;
}

.info-container {
    color: grey;
    padding: 1em .5em 1em 3em;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-between;
    height: 100%;
}

.profile-img {
    width: 20%;
}

.grey-underline {
    border-bottom: grey 2px solid;
    border-radius: 0;
    padding: 0;
}
</style>