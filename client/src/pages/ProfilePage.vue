<template>
    <div class="component">


    </div>
</template>


<script>
import { onMounted } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';

export default {
    props: {
        profileId: { type: Number, required: true }
    },
    setup(props) {
        // VARIABLES
        let vaults
        let keeps
        let profile
        // FUNCTIONS
        async function getVaults() {
            try {
                // TODO
            } catch (error) {
                Pop.error(error)
            }
        }
        async function getKeeps() {
            try {
                // TODO
            } catch (error) {
                Pop.error(error)
            }
        }
        async function getProfile() {
            try {
                // TODO
            } catch (error) {
                Pop.error(error)
            }
        }
        // LIFECYCLE
        onMounted(() => {
            getVaults()
            getKeeps()
            getProfile()
        })
        return { vaults, keeps, profile }
    }
};
</script>


<style lang="scss" scoped></style>