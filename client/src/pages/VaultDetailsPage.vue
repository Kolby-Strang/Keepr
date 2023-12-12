<template>
    <div v-if="vault.id || vaultKeeps.length > 0" class="container mt-3">
        <div class="row">
            <div class="col-12 d-flex flex-column align-items-center">
                <div>
                    <div class="position-relative">
                        <img class="vault-img rounded" :src="vault.img" :alt="`Image for vault ${vault.name}`">
                        <div class="vault-img-info">
                            <p class="fs-2 mb-0 space-letters">{{ vault.name }}</p>
                            <p>by {{ vault.creator?.name }}</p>
                        </div>
                    </div>
                    <div class="dropdown text-end">
                        <button title="Vault options" class="btn p-0" type="button" data-bs-toggle="dropdown"
                            aria-expanded="false">
                            <i class="mdi mdi-dots-horizontal fs-4"></i>
                        </button>
                        <ul class="dropdown-menu">
                            <li>
                                <p data-bs-target="#editVaultModal" data-bs-toggle="modal"
                                    class="dropdown-item mb-0 selectable">Edit Vault</p>
                                <p @click="destroyVault()" class="dropdown-item mb-0 text-danger selectable">Delete Vault
                                </p>
                            </li>
                        </ul>
                    </div>
                    <div class="d-flex justify-content-center">
                        <p class="bg-dark keep-count rounded-pill px-2">
                            {{ `${vaultKeeps.length} ${vaultKeeps.length == 1 ? 'Keep' : 'Keeps'}` }}
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-12 masonry-with-columns">
                <div v-for="vaultKeep in vaultKeeps" :key="vaultKeep.id">
                    <VaultKeepCard :vaultKeep="vaultKeep" />
                </div>
            </div>
        </div>
    </div>
    <Loader v-else />
    <VaultKeepModal />
    <EditVaultModal />
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { computed, ref, watch } from 'vue';
import Pop from '../utils/Pop';
import { vaultKeepsService } from '../services/VaultKeepsService';
import { vaultsService } from '../services/VaultsService';
import VaultKeepCard from '../components/VaultKeepCard.vue';
import VaultKeepModal from '../components/modals/VaultKeepModal.vue';
import { AppState } from '../AppState';
import EditVaultModal from '../components/modals/EditVaultModal.vue';
import Loader from '../components/Loader.vue';
export default {
    setup() {
        // VARIABLES
        const route = useRoute();
        const router = useRouter()
        const watchableVaultId = computed(() => route.params.vaultId);
        const vault = computed(() => AppState.activeVault);
        const vaultKeeps = computed(() => AppState.activeVaultKeeps);
        // FUNCTIONS
        async function getVault() {
            try {
                await vaultsService.getVaultById(watchableVaultId.value);
            }
            catch (error) {
                router.push('/')
                Pop.error(error);
            }
        }
        async function getVaultKeeps() {
            try {
                await vaultKeepsService.getVaultKeepsByVaultId(watchableVaultId.value);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function destroyVault() {
            try {
                const yes = await Pop.confirm()
                if (!yes) {
                    Pop.toast("Cancelled")
                    return
                }
                await vaultsService.destroyVault(watchableVaultId.value)
                Pop.success(`${vault.value.name} was deleted!`)
                router.push('/')
            } catch (error) {
                Pop.error(error)
            }
        }
        // LIFECYCLE
        watch(watchableVaultId, () => {
            getVault();
            getVaultKeeps();
        }, { immediate: true });
        return { vault, vaultKeeps, destroyVault };
    },
    components: { VaultKeepCard, VaultKeepModal, EditVaultModal, Loader }
};
</script>


<style lang="scss" scoped>
.vault-img-info {
    position: absolute;
    top: 0;
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: end;
    color: white;
    text-shadow: 0 0 10px black;
    font-weight: bold;
}

.vault-img {
    max-width: 100%;
    height: 25vh;
    aspect-ratio: 19/9;
    object-fit: cover;
    object-position: center;
}

.space-letters {
    letter-spacing: .2em;
}

.keep-count {
    color: white;
    width: max-content;
}
</style>