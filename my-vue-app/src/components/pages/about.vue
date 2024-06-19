<template>
    <div>
        <!-- Banner -->
        <banner />
        <!-- Main -->
        <div class=" px-12">
            <!-- Service -->
            <service />
        </div>
    </div>
</template>

<script>
import banner from '../about/banner.vue'
import service from '../about/service.vue'
export default {
    components: { banner, service },

    mounted() {
        // Cuộn trang lên đầu khi component được mount
        window.scrollTo(0, 0);
    },
}
</script>

<style></style>