<template>
    <div class="modal fade" :id="id" tabindex="-1" :aria-labelledby="`${id}Label`" aria-hidden="true">
        <div :class="`modal-dialog ${size}`">
            <div class="modal-content">
                <slot>
                    <!-- Modal Content Here -->
                </slot>
            </div>
        </div>
    </div>
</template>


<script>

export default {
    props: {
        size: { type: String, default: '' },
        id: { type: String, required: true }
    }
};
</script>


<style lang="scss" scoped></style>