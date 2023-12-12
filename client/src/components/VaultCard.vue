<template>
    <!-- TODO link this to the vault details page -->
    <div class="position-relative box-shadow rounded selectable">
        <img class="vault-img rounded" :src="vault.img" :alt="`Image for ${vault.name}`">
        <div class="vault-card-info rounded">
            <!-- TODO add some bottom shadow (see mock) -->
            <div class="d-flex justify-content-between align-items-center">
                <p class="fs-4 fw-bold mb-0 ms-2 over-none text-nowrap">{{ vault.name.length > 14 ? vault.name.substring(0,
                    11) + '...' : vault.name }}</p>
                <i v-if="vault.isPrivate" class="mdi mdi-lock-outline fs-4"></i>
            </div>
        </div>
    </div>
</template>


<script>
import { Vault } from '../models/Vault';

export default {
    props: {
        vault: { type: Vault, required: true }
    },
    setup() {
        return {}
    }
};
</script>


<style lang="scss" scoped>
.vault-img {
    aspect-ratio: 15/9;
    width: 100%;
    object-fit: cover;
    object-position: center;
}

.vault-card-info {
    position: absolute;
    top: 0;
    height: 100%;
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: end;
    color: white;
    background: linear-gradient(0deg, rgba(0, 0, 0, 0.761) 0%, rgba(0, 0, 0, 0.67725315467984069) 20%, rgba(255, 255, 255, 0) 35%, rgba(255, 255, 255, 0) 100%);
}
</style>