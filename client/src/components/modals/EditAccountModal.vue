<template>
    <ModalBase class="text-primary" id="editAccountModal">
        <div class="p-3">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <p class="fs-2 fw-bold mb-0">Edit Account</p>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="mb-3">
                <input v-model="accountEditable.name" minlength="1" maxlength="255" type="text" class="form-control"
                    placeholder="Name...">
                <div class="form-text">*Please use your real name</div>
            </div>
            <div class="mb-3">
                <input v-model="accountEditable.picture" minlength="1" maxlength="1000" type="text" class="form-control"
                    placeholder="Profile Picture...">
            </div>
            <div class="mb-5">
                <input v-model="accountEditable.coverImg" minlength="1" maxlength="1000" type="text" class="form-control"
                    placeholder="Cover Photo...">
            </div>
            <div class="d-flex justify-content-between align-items-end">
                <div class="text-center w-25">
                    <p>Profile Pic</p>
                    <img class="profile-img w-100" :src="accountEditable.picture" alt="">
                </div>
                <div class="text-center w-50">
                    <p>Cover Img</p>
                    <img class="cover-img w-100 rounded" :src="accountEditable.coverImg" alt="">
                </div>
                <button @click="editAccount()" class="btn btn-dark">Submit!</button>
            </div>
        </div>
    </ModalBase>
</template>


<script>
import { computed, onMounted, ref, watch } from 'vue';
import ModalBase from './ModalBase.vue';
import { AppState } from '../../AppState';
import Pop from '../../utils/Pop';
import { Modal } from 'bootstrap';
import { accountService } from '../../services/AccountService';

export default {
    setup() {
        // VARIABLES
        const accountEditable = ref({})
        const watchableAccount = computed(() => AppState.account)
        // FUNCTIONS
        async function editAccount() {
            try {
                await accountService.editAccount(accountEditable.value)
                Modal.getOrCreateInstance('#editAccountModal').hide()
                Pop.success('Account Edited!')
            } catch (error) {
                Pop.error(error)
            }
        }
        // LIFECYCLE
        watch(watchableAccount, () => {
            accountEditable.value = { ...AppState.account }
        }, { immediate: true })
        return {
            accountEditable,
            editAccount
        };
    },
    components: { ModalBase }
};
</script>


<style lang="scss" scoped>
input {
    border: 0;
    outline: 0;
    border-radius: .5em;
}

.btn {
    height: min-content;
}
</style>