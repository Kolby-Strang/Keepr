<template>
    <ModalBase class="container-fluid" size="modal-xl" id="keepDetails">
        <div v-if="keep.id" class="row">
            <div class="col-12 col-md-6 pe-md-0 position-relative">
                <img class="keep-img rounded-top rounded-sm-start" :src="keep.img" :alt="`Image for keep ${keep.name}`">
                <div v-if="keep.creator.id == account.id" class="position-absolute t-0 r-0 w-100 h-100 text-end p-2 px-3">
                    <i @click="destroyKeep()" class="mdi mdi-trash-can-outline fs-3 text-danger text-shadow selectable"></i>
                </div>
            </div>
            <div class="col-12 col-md-6 ps-md-0">
                <div class="info-container">

                    <div class="d-flex align-items-center pe-4">
                        <i class="mdi mdi-eye-outline fs-4 m-1"></i>
                        {{ keep.views }}
                        <i class="mdi mdi-alpha-k-box-outline fs-4 m-1 ms-3"></i>
                        {{ keep.kept }}
                    </div>
                    <div class="pe-4">
                        <p class="fs-1 text-dark fw-bold text-center">{{ keep.name }}</p>
                        <p>{{ keep.description }}</p>
                    </div>
                    <div class="row w-100 pe-4 pe-sm-2 pe-md-1">
                        <div class="col-12 col-sm-6 col-md-12 col-lg-5 p-0">
                            <div v-if="userVaults.length > 0" class="d-flex justify-content-center align-items-center">
                                <!-- TODO make card hand type selector -->
                                <select v-model="editableVaultId" class="w-75">
                                    <option value="" selected disabled hidden>Vaults...</option>
                                    <option v-for="vault in userVaults" :key="vault.id" :value="vault.id">
                                        {{ vault.name }}
                                    </option>
                                </select>
                                <button @click="saveActiveKeepToVault()" role="button"
                                    class="btn btn-primary text-white p-1 py-0 ms-2 selectable">save</button>
                            </div>
                        </div>
                        <div class="col-12 col-sm-6 col-md-12 col-lg-7 p-0 mt-2 mt-sm-0 mt-md-2 mt-lg-0">
                            <router-link @click="dismissModal()"
                                :to="{ name: 'ProfilePage', params: { 'profileId': keep.creator.id } }"
                                class="d-flex justify-content-center align-items-center">
                                <img class="profile-img" :src="keep.creator.picture" :alt="keep.creator.name">
                                <p class="mb-0 ms-1 d-inline text-dark fw-bold">{{ keep.creator.name.length > 15 ?
                                    keep.creator.name.substring(0, 12) + '...' : keep.creator.name }}</p>
                            </router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ModalBase>
</template>


<script>
import { computed, ref } from 'vue'
import { AppState } from '../../AppState'
import ModalBase from './ModalBase.vue'
import Pop from '../../utils/Pop';
import { vaultsService } from '../../services/VaultsService';
import { Modal } from 'bootstrap';
import { keepsService } from '../../services/KeepsService';

export default {
    setup() {
        // VARIABLES
        const keep = computed(() => AppState.activeKeep)
        const userVaults = computed(() => AppState.activeUserVaults)
        const editableVaultId = ref()
        // FUNCTIONS
        async function saveActiveKeepToVault() {
            try {
                if (editableVaultId.value == null) {
                    Pop.toast('Please choose a Vault first!')
                    return
                }
                await vaultsService.saveActiveKeepToVault(editableVaultId.value)
                Pop.success(`${keep.value.name} was saved to ${userVaults.value.find(v => v.id == editableVaultId.value)?.name}`)
            } catch (error) {
                Pop.error(error)
            }
        }
        function dismissModal() {
            Modal.getOrCreateInstance('#keepDetails').hide()
        }
        async function destroyKeep() {
            try {
                const yes = await Pop.confirm()
                if (!yes) {
                    Pop.toast('Cancelled')
                    return
                }
                await keepsService.destroyKeep(keep.value.id)
                Pop.success(`Keep was Deleted!`)
                Modal.getOrCreateInstance('#keepDetails').hide()
            } catch (error) {
                Pop.error
            }
        }
        // LIFECYCLE
        return {
            keep,
            account: computed(() => AppState.account),
            userVaults,
            editableVaultId,
            saveActiveKeepToVault,
            dismissModal,
            destroyKeep
        }
    },
    components: { ModalBase }
};
</script>


<style lang="scss" scoped>
select {
    border: 0;
    outline: none !important;
    border-radius: 0;
    border-bottom: 2px solid rgb(197, 197, 197);
    padding: 0;
}

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
</style>