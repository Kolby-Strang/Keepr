<template>
    <div v-if="profile.id" class="container">
        <div class="row mt-3 px-0 mx-0 px-md-5 mx-md-5">
            <div class="col-12 profile-cover-container">
                <img class="cover-img rounded"
                    :src="profile.coverImg.length > 1 ? profile.coverImg : 'https://cdn.vectorstock.com/i/preview-1x/65/30/default-image-icon-missing-picture-page-vector-40546530.jpg'"
                    onerror="this.src='https://cdn.vectorstock.com/i/preview-1x/65/30/default-image-icon-missing-picture-page-vector-40546530.jpg'"
                    :alt="`Cover image for ${profile.name}`">
                <img class="profile-img box-shadow"
                    :src="profile.picture.length > 1 ? profile.picture : 'https://tr.rbxcdn.com/70108dc7da4e002c8e5d2c1dcf0825fb/420/420/Hat/Png'"
                    onerror="this.src='https://tr.rbxcdn.com/70108dc7da4e002c8e5d2c1dcf0825fb/420/420/Hat/Png'"
                    :alt="`Profile image for ${profile.name}`">
            </div>
            <div class="col-12 text-end">
                <div v-if="account.id == profile.id">
                    <i class="mdi mdi-pen text-primary fs-1" role="button" title="Edit Account" data-bs-toggle="modal"
                        data-bs-target="#editAccountModal">
                    </i>
                </div>
            </div>
            <br><br><br>
            <div class="col-12 text-center">
                <p class="fs-1 fw-bold mb-0">{{ profile.name }}</p>
                <p>
                    {{ vaults.length + ' ' + (vaults.length == 1 ? 'Vault' : 'Vaults') }}
                    |
                    {{ keeps.length + ' ' + (keeps.length == 1 ? 'Keep' : 'Keeps') }}
                </p>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <p class="fs-2 fw-bold">Vaults</p>
            </div>
            <div v-for="vault in vaults" :key="vault.id" class="col-6 col-md-4 col-lg-3 mb-4">
                <VaultCard :vault="vault" />
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <p class="fs-2 fw-bold">Keeps</p>
                <div class="masonry-with-columns">
                    <div v-if="keeps.length > 0" v-for="keep in keeps" :key="keep.id">
                        <KeepCard :keep="keep" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <Loader v-else />
    <EditAccountModal />
</template>


<script>
import { computed, onMounted, ref, watch } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { profilesService } from '../services/ProfilesService';
import { useRoute } from 'vue-router';
import { keepsService } from '../services/KeepsService';
import KeepCard from '../components/KeepCard.vue';
import VaultCard from '../components/VaultCard.vue';
import EditAccountModal from '../components/modals/EditAccountModal.vue'
import Loader from '../components/Loader.vue';

export default {
    setup(props) {
        // VARIABLES
        const route = useRoute();
        const vaults = ref([]);
        const keeps = ref([]);
        const profile = ref({});
        const watchableAccount = computed(() => AppState.account)
        const watchableProfileId = computed(() => route.params.profileId)
        const watchableUserKeeps = computed(() => AppState.activeUserKeeps)
        const watchableUserVaults = computed(() => AppState.activeUserVaults)
        // FUNCTIONS
        async function getVaults() {
            try {
                vaults.value = await vaultsService.getVaultsByProfileId(
                    route.params.profileId, profile.value.id == watchableAccount.value.id);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getKeeps() {
            try {
                keeps.value = await keepsService.getKeepsByProfileId(
                    route.params.profileId, profile.value.id == watchableAccount.value.id);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getProfile() {
            try {
                profile.value = await profilesService.getProfileByProfileId(route.params.profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        // LIFECYCLE
        watch(watchableUserKeeps, () => {
            keeps.value = watchableUserKeeps.value
        })
        watch(watchableUserVaults, () => {
            vaults.value = watchableUserVaults.value
        })
        watch(watchableProfileId, () => {
            profile.value = {}
            vaults.value = []
            keeps.value = []
            getVaults();
            getKeeps();
            getProfile();
        }, { immediate: true })
        onMounted(() => {
            watch(watchableAccount, () => {
                if (watchableAccount.value.id == profile.value.id) {
                    profile.value = { ...watchableAccount.value }
                }
            })
        });
        return {
            vaults,
            keeps,
            profile,
            account: computed(() => AppState.account)
        };
    },
    components: { KeepCard, VaultCard, EditAccountModal, Loader }
};
</script>


<style lang="scss" scoped>
.profile-cover-container {
    position: relative;
    left: 50%;
    transform: translate(-50%, 0);
    display: flex;
    justify-content: center;
}

.profile-img {
    position: absolute;
    bottom: -4em;
    width: 15vh;
    border: white solid 2px;
}
</style>