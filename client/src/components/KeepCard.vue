<template>
    <div @click="openKeepDetailsModal()" class="rounded position-relative box-shadow selectable">
        <img class="img-fluid rounded" :src="keep.img"
            onerror="this.src='https://cdn.vectorstock.com/i/preview-1x/65/30/default-image-icon-missing-picture-page-vector-40546530.jpg'"
            :alt="keep.name + ' image'">
        <div class="position-absolute h-100 w-100 d-flex justify-content-between align-items-end t-0 p-2">
            <p class="text-white mb-0 fw-bold text-shadow fs-5 over-none">{{ keep.name }}</p>
            <img class="profile-img w-25 d-none d-md-block" :src="keep.creator.picture"
                onerror="this.src='https://tr.rbxcdn.com/70108dc7da4e002c8e5d2c1dcf0825fb/420/420/Hat/Png'"
                :alt="keep.creator.name" :title="keep.creator.name">
        </div>
    </div>
</template>


<script>
import { Modal } from 'bootstrap';
import { Keep } from '../models/Keep';
import { keepsService } from '../services/KeepsService';


export default {
    props: {
        keep: { type: Keep, required: true }
    },
    setup(props) {
        // FUNCTIONS
        async function openKeepDetailsModal() {
            const keep = await keepsService.getKeepByIdAndUpdateAppState(props.keep.id)
            keepsService.setActiveKeep(keep)
            Modal.getOrCreateInstance('#keepDetails').show()
        }
        return { openKeepDetailsModal }
    }
};
</script>


<style lang="scss" scoped></style>